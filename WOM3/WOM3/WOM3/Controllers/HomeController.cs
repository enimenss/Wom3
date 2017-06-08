using Microsoft.AspNet.SignalR;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WOM3.DAL;
using WOM3.Models;
using WOM3;

namespace WOM3.Controllers
{
    public class HomeController : Controller
    {
        private WOMContext db = new WOMContext();

        private readonly static ConnectionMapping<string> _connections = ConnectionMapping<string>.GetConection;


        public ActionResult Profiles(string id,int? page)
        {

            if (Session["user"] == null || String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Welcome");
            }
            ViewBag.page = page;
            
            User user = db.Users.Where(b => b.Username == id).FirstOrDefault();
            string target = (string)Session["user"];
            int count = db.FriendRequests.Where(f => f.UsernameId ==target ).Count();
            ViewBag.FriendRequests = count;
            ViewBag.ownNews = user.NumOfNews;
            if (Session["user"].ToString() != id)
            {
                User own = db.Users.Find(Session["user"].ToString());
                ViewBag.ownNews = own.NumOfNews;

            }
            count= db.Friends.Where(f => f.UsernameId==target && f.FriendId==id).Count();
          
                ViewBag.Friend = count;

           count = db.FriendRequests.Where(f => f.UsernameId == id && f.FriendRId==target).Count();
            ViewBag.FR = count;

            var s = (from p in db.Friends where (p.UsernameId == target) select p).ToList();
            List<OnlineFriends> lista = new List<OnlineFriends>();
            foreach(var friends in s)
            {
                OnlineFriends friend = new OnlineFriends() { Username = friends.FriendId,Avatar=friends.Friend.Avatar, isOnline = false };
                if (_connections.GetConnections(friends.FriendId).Count() > 0)
                {
                    friend.isOnline = true;

                }
                lista.Add(friend);
           }
            TempData["Online"] = lista;

            if (Session["user"].ToString() != id)
            {
                FriendRequests poslaoZahtev = db.FriendRequests.Where(x => x.UsernameId == target && x.FriendRId == id).FirstOrDefault();
                if (poslaoZahtev != null)
                {
                    ViewBag.poslaoZahtev = 1;
                }
                else
                {
                    ViewBag.poslaoZahtev = 0;
                }
            }

            return View(user);
        }

        [HttpPost]
        public void Profiles(FormCollection param)
        {
            string ime = (string)Session["user"];
            User user = db.Users.Find(ime);
            int id = Int32.Parse(param["id_Itema"]);
            UserItems item = db.UserItems.Where(x => x.Username == ime && x.ItemID == id).FirstOrDefault();
            if (item != null)
            {
                int gold = user.UserStats.Gold +(int)(((double)item.Item.Price)*0.85);
                user.UserStats.Gold = gold;
                db.Entry(user.UserStats).Property(x => x.Gold).CurrentValue = gold;
                db.UserItems.Remove(item);
                db.SaveChanges();

            }
            Response.Redirect("~/Home/Profiles/"+ime);

        }
        public ActionResult GotToShop(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return RedirectToAction("Index", "Welcome");
            }
            Session["user"] = u.Username;
            return RedirectToAction("Shop", "Home");
        }
        public ActionResult GotToNews(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return RedirectToAction("Index", "Welcome");
            }
            Session["user"] = u.Username;
            return RedirectToAction("News", "Home");
        }
        public ActionResult UserItems(string username,int? page)
        {
            var itemi = db.UserItems.Where(x => x.Username == username).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            itemi.OrderBy(x => x.Item.Armor);
            ViewBag.Username = username;
            return PartialView(itemi.ToPagedList(pageNumber, pageSize));
        }




        public ActionResult result(string sort, int? page, string SearchString, string prevSearch)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");
            }

            var user = db.Users.Find(Session["user"]);
            ViewBag.News = user.NumOfNews;

            int count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();
            ViewBag.FriendRequests = count;

            var stats = from s in db.UserStats select s;
            if ((SearchString == null || SearchString == "") && prevSearch != null)
            {
                SearchString = prevSearch;

            }


            ViewBag.Search = SearchString;

            if (!String.IsNullOrEmpty(SearchString))
            {
                stats = stats.Where(s => s.Username.Contains(SearchString));
                                       
            }


            if (sort == null)
            {
                stats = stats.OrderByDescending(s => s.Wins+s.Loses);
                ViewBag.sort = "Total";
            }
            else
            {
                ViewBag.sort = sort;
                switch (sort)
                {
                    case "Win":
                        stats = stats.OrderByDescending(s => s.Wins);
                        break;
                    case "Lose":
                        stats = stats.OrderByDescending(s => s.Loses);
                        break;
                    default:
                        stats = stats.OrderByDescending(s => s.Wins+s.Loses);
                        break;

                }

            }
            TempData["stats"] = stats;
            int pageSize = 13;
            int pageNumber = (page ?? 1);
            return View(stats.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Shoping(int?page,FormCollection param)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }

            if(param["page"]!=null)
            page = Int32.Parse(param["page"]);

            string ID = (string)Session["user"];
            User user = db.Users.Include("UserStats").Include("UserItems").Single(c => c.Username == ID);

            int count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();
            ViewBag.FriendRequests = count;

            if (param["kupiId"] != null)
            {
                int id = Int32.Parse(param["kupiId"]);
                UserItems dodaj = new UserItems();
                    Items item = db.Items.Find(id);
                    dodaj.ItemID = id;
                    dodaj.Username = ID;
                    dodaj.DateOfPurchase = DateTime.Now;
                    db.UserItems.Add(dodaj);
                    db.SaveChanges();
                    int gold = user.UserStats.Gold - item.Price;
                    user.UserStats.Gold = gold;
                    db.Entry(user.UserStats).Property(x => x.Gold).CurrentValue = gold;
                db.SaveChanges();
            }

            var Items = (from item in db.Items select item).ToList();
            foreach(UserItems i in user.UserItems)
            {
                Items.Remove(i.Item);
            }
           Items.OrderBy(x => x.Price);
            TempData["user"] = user;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Items.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult logout()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
            }
            return RedirectToAction("Index", "Welcome");
        }





       


        public ActionResult Message(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            string my = (string)Session["user"];


            var user = db.Users.Find(Session["user"]);
            ViewBag.News = user.NumOfNews;

            int count = db.FriendRequests.Where(f => f.UsernameId ==my).Count();
            ViewBag.FriendRequests = count;

            var friends = db.Friends.Include("User").Where(s => s.UsernameId == my).ToList();
            if (friends.Count > 0)
            {

                TempData["friends"] = friends;
                if (String.IsNullOrEmpty(id) || id == my)
                {
                    id = friends.First().FriendId;

                }
                TempData["Friend"] = id;
            }
            var poruke = db.Messages.Where(x => (x.UsernameId == my && x.ReceiverId == id) || (x.UsernameId == id && x.ReceiverId == my));
            poruke = poruke.OrderBy(x => x.Datum);
            return View(poruke);
        }


        public ActionResult News(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            var user = db.Users.Find(Session["user"]);
            int count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();
            ViewBag.FriendRequests = count;
            db.Entry(user).Property(x => x.NumOfNews).CurrentValue =0;
            db.SaveChanges();

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var news = from s in db.News select s;
            news=news.OrderByDescending(x => x.Datum);
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PostNews()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            return View();
        }


        [HttpGet]
        public ActionResult getM()
        {
            List<PrimljenaPoruka> list = new List<PrimljenaPoruka>();
            string e = (string)Session["user"];
            var friends = (from s in db.Friends where s.UsernameId == e select s.FriendId).ToList();
            foreach (var friend in friends)
            {
                var mesage = db.Messages.Include("User").Where(x => x.ReceiverId == e && x.UsernameId == friend).OrderByDescending(x => x.Datum).FirstOrDefault();
                if (mesage != null)
                {
                    PrimljenaPoruka poruka = new PrimljenaPoruka() { Message = mesage.Message, Sender = mesage.UsernameId, Jpg = mesage.User.Avatar, Datum = mesage.Datum.ToString() };
                    list.Add(poruka);
                }
            }
            list = list.OrderByDescending(x => x.Datum).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult getRequests()
        {
            List <ZahtevZaPrijateljstvo> list = new List<ZahtevZaPrijateljstvo>();
            var jsonSerialiser = new JavaScriptSerializer();
            string e = (string)Session["user"];
            var friend =db.FriendRequests.Where(b => b.UsernameId ==e ).ToList();
            foreach(var s in friend)
            {
                ZahtevZaPrijateljstvo zahtev = new ZahtevZaPrijateljstvo() { Username = s.FriendRId, Avatar = s.FriendR.Avatar };
                list.Add(zahtev);
            }

            var json = jsonSerialiser.Serialize(list);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}