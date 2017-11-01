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


        public ActionResult Profiles(string id, int? page)
        {

            if (Session["user"] == null || String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Welcome");
            }
            ViewBag.page = page;

            User user = db.Users.Where(b => b.Username == id).FirstOrDefault();
            string target = (string)Session["user"]; //bolje my nek se zove

            int count = db.Friends.Where(f => f.UsernameId == target && f.FriendId == id).Count();

            ViewBag.Friend = count;

            count = db.FriendRequests.Where(f => f.UsernameId == id && f.FriendRId == target).Count();
            ViewBag.FR = count;


            List<OnlineFriends> lista = db.Friends.Where(
                                         x => x.UsernameId == target).ToList().Select(p =>
                                           new OnlineFriends
                                           {
                                               Username = p.FriendId,
                                               Avatar = p.Friend.Avatar,
                                               isOnline = (bool)(_connections.GetConnections(p.FriendId).Count() > 0) ? true : false,
                                               isInGame=(bool)(_connections.GetConnections(p.FriendId).Where(x=>x.Source=="game").Count()>0)? true : false
                                           }).ToList();
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

        public ActionResult Test()
        {
            return View();
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
                int gold = user.UserStats.Gold + (int)(((double)item.Item.Price) * 0.85);
                user.UserStats.Gold = gold;
                db.Entry(user.UserStats).Property(x => x.Gold).CurrentValue = gold; //proveri rad
                db.UserItems.Remove(item);
                db.SaveChanges();

            }
            Response.Redirect("~/Home/Profiles/" + ime);

        }
        public ActionResult GotToShop(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return RedirectToAction("Index", "Welcome");
            }
            Session["user"] = u.Username;
            return RedirectToAction("Shoping", "Home");
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
        public ActionResult UserItems(string username, int? page)
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
                stats = stats.OrderByDescending(s => s.Wins + s.Loses);
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
                        stats = stats.OrderByDescending(s => s.Wins + s.Loses);
                        break;

                }

            }
         
            int pageSize = 13;
            int pageNumber = (page ?? 1);
            return View(stats.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Shoping(int? page, FormCollection param)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            return View();

            //if (param["page"] != null)
            //    page = Int32.Parse(param["page"]);

            //string ID = (string)Session["user"];
            //User user = db.Users.Include("UserStats").Include("UserItems").Single(c => c.Username == ID);

            ////int count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();
            ////ViewBag.FriendRequests = count;

            //if (param["kupiId"] != null)
            //{
            //    int id = Int32.Parse(param["kupiId"]);
            //    UserItems dodaj = new UserItems();
            //    Items item = db.Items.Find(id);
            //    dodaj.ItemID = id;
            //    dodaj.Username = ID;
            //    dodaj.DateOfPurchase = DateTime.Now;
            //    db.UserItems.Add(dodaj);
            //    db.SaveChanges();
            //    int gold = user.UserStats.Gold - item.Price;
            //    user.UserStats.Gold = gold;
            //    db.Entry(user.UserStats).Property(x => x.Gold).CurrentValue = gold; //sigurno da se menja
            //    db.SaveChanges();
            //}

            //var Items = (from item in db.Items select item).ToList(); ///cudno
            //foreach (UserItems i in user.UserItems)
            //{
            //    Items.Remove(i.Item);
            //}
            //Items.OrderBy(x => x.Price);
            //TempData["user"] = user; //zbog golda trenutno
            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            //return View(Items.ToPagedList(pageNumber, pageSize));

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
            OnlineFriends UserView = new OnlineFriends { Username = user.Username, Avatar = user.Avatar };
            TempData["Me"] = UserView;

            var friends = db.Friends.Where(s => s.UsernameId == my).ToList().Select(x => new OnlineFriends
            {
                Username = x.Friend.Username,
                Avatar = x.Friend.Avatar,
                isOnline = (bool)(_connections.GetConnections(x.Friend.Username).Count() > 0) ? true : false,
                isInGame = (bool)(_connections.GetConnections(x.Friend.Username).Where(y => y.Source == "game").Count() > 0) ? true : false,
                NumOfNotSeen = db.Messages.Where(t => t.ReceiverId == my && t.UsernameId == x.FriendId && t.isRead == 0).OrderByDescending(t => t.Datum).ToList().Count(),
                Message =new ChatMessage( db.Messages.Where(t => (t.ReceiverId == my && t.UsernameId == x.FriendId) || (t.ReceiverId == x.FriendId && x.UsernameId == my)).OrderByDescending(t => t.Datum).FirstOrDefault())


        }).OrderByDescending(x=>x.Message.Datum).ToList();

            if (friends.Count() > 0)
            {

                TempData["friends"] = friends;
                if (String.IsNullOrEmpty(id) || id == my)
                {
                    id = friends.First().Username;

                }
                var friend = (from u in db.Users where u.Username == id select new OnlineFriends { Username = u.Username, Avatar = u.Avatar }).SingleOrDefault();
                TempData["Friend"] = friend;
            }
            var poruke = db.Messages.Where(x => (x.UsernameId == my && x.ReceiverId == id) || (x.UsernameId == id && x.ReceiverId == my));
            poruke = poruke.OrderBy(x => x.Datum);

            var lastMessage= db.Messages.Where(x =>(x.UsernameId == my && x.ReceiverId == id && x.isRead==1)).OrderByDescending(x=>x.Datum).FirstOrDefault();
            int SeenId = 0;
            if (lastMessage != null)
            {
                SeenId = lastMessage.id;
            }
            TempData["SeenId"] = SeenId;
            return View(poruke);
        }


        public ActionResult News(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            var user = db.Users.Find(Session["user"]);
            db.Entry(user).Property(x => x.NumOfNews).CurrentValue = 0; //hmm 
            db.SaveChanges();

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var news = from s in db.News select s;
            news = news.OrderByDescending(x => x.Datum);
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

        [NonAction]
        public void VratiNotifikacije(out int count,out int News,out int NumOfMessages )
        {
            count = 0;
            News = 0;
            NumOfMessages = 0;

            if (Session["user"] == null)
            {
                return;

            }
            var user = db.Users.Find(Session["user"]);

            News = user.NumOfNews;

             count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();

            string my = (string)Session["user"];
            var friends = (from s in db.Friends where s.UsernameId == my select s.FriendId).ToList();
            foreach (var friend in friends)
            {
                var mesage = db.Messages.Where(x => x.ReceiverId == my && x.UsernameId == friend).OrderByDescending(x => x.Datum).FirstOrDefault();
                if (mesage != null && mesage.isRead == 0)
                {
                    NumOfMessages = NumOfMessages + 1;
                }

            }
        }

        public JsonResult NewsAndRequests()
        {
            if (Session["user"] == null)
            {
                return Json(false);

            }
            var user = db.Users.Find(Session["user"]);
            int count = db.FriendRequests.Where(f => f.UsernameId == user.Username).Count();

            int NumOfMessages = 0;

            string my = (string)Session["user"];
            var friends = (from s in db.Friends where s.UsernameId == my select s.FriendId).ToList();
            foreach (var friend in friends)
            {
                var mesage = db.Messages.Where(x => x.ReceiverId == my && x.UsernameId == friend).OrderByDescending(x => x.Datum).FirstOrDefault();
                if (mesage != null && mesage.isRead == 0)
                {
                    NumOfMessages = NumOfMessages + 1;
                }

            }

            var data = new
            {
                News = user.NumOfNews,
                NumRequests = count,
                NumOfMessages
            };
            return Json(data);
        }


        //[HttpGet]
        //public ActionResult getM()  //prepravi
        //{
        //    List<PrimljenaPoruka> list = new List<PrimljenaPoruka>();
        //    string e = (string)Session["user"];
        //    var friends = (from s in db.Friends where s.UsernameId == e select s.FriendId).ToList();
        //    foreach (var friend in friends)
        //    {
        //        var mesage = db.Messages.Include("User").Where(x => x.ReceiverId == e && x.UsernameId == friend).OrderByDescending(x => x.Datum).FirstOrDefault();
        //        if (mesage != null)
        //        {
        //            PrimljenaPoruka poruka = new PrimljenaPoruka() { Message = mesage.Message, Sender = mesage.UsernameId, Jpg = mesage.User.Avatar, Datum = mesage.Datum.ToString() };
        //            list.Add(poruka);
        //        }
        //    }
        //    list = list.OrderByDescending(x => x.Datum).ToList();
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(list);
        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}


        [HttpPost]
        public JsonResult getM()
        {
            List<PrimljenaPoruka> list = new List<PrimljenaPoruka>();

            string my = (string)Session["user"];
            var friends = (from s in db.Friends where s.UsernameId == my select s.FriendId).ToList();
            foreach (var friend in friends)
            {
                var mesage = db.Messages.Where(x => (x.ReceiverId == my && x.UsernameId == friend) || ( x.ReceiverId == friend && x.UsernameId == my)).OrderByDescending(x => x.Datum).FirstOrDefault();
                if (mesage != null)
                //{
                //    PrimljenaPoruka poruka = new PrimljenaPoruka() { Message = mesage.Message, Sender = mesage.UsernameId, Jpg = mesage.User.Avatar, Datum = mesage.Datum.ToString(), IsSeen = mesage.isRead };
                //    if (poruka.Sender == my)
                //    {
                //        poruka.Message = mesage.Receiver.Avatar;
                //        poruka.IsSeen = 0; //za buduce primene gledaj da iskljucis evente za svoje poruke npr IsSeen=2
                //    }

                {
                    PrimljenaPoruka poruka;
                    if (mesage.UsernameId == my)
                    {
                        //za buduce primene gledaj da iskljucis evente za svoje poruke npr IsSeen=2
                       poruka = new PrimljenaPoruka { Message = mesage.Message, Sender = mesage.ReceiverId, Jpg = mesage.Receiver.Avatar, Datum = mesage.Datum.ToString(), IsSeen = 1 ,NumOfNotSeen=0};
                    }
                    else
                    {
                        poruka = new PrimljenaPoruka() { Message = mesage.Message, Sender = mesage.UsernameId, Jpg = mesage.User.Avatar, Datum = mesage.Datum.ToString(), IsSeen = mesage.isRead };
                        poruka.NumOfNotSeen = db.Messages.Where(x => x.ReceiverId == my && x.UsernameId == friend && x.isRead == 0).OrderByDescending(x => x.Datum).ToList().Count();
                    }
                    poruka.Online = (bool)(_connections.GetConnections(friend).Count() > 0) ? true : false;
                
                    list.Add(poruka);
                }
            }
            list = list.OrderByDescending(x => x.Datum).ToList();
            return Json(list);

        }



        [HttpPost]
        public ActionResult getRequests()
        {
           

            string my = (string)Session["user"];
            var friend = (from f in db.FriendRequests
                          where f.UsernameId == my
                          select new
                          {
                              Username = f.FriendRId,
                              Avatar = f.FriendR.Avatar,
                              Datum = f.Datum.ToString()
                          }).ToList().OrderBy(x=>x.Datum);
            return Json(friend);

        }


        [HttpPost]
        public JsonResult IsSeen(string friend)
        {
            string my = (string)Session["user"];

            var mesage = db.Messages.Where(x => x.ReceiverId == my && x.UsernameId == friend).OrderByDescending(x => x.Datum).ToList().ElementAt(1);
            if (mesage == null || mesage.isRead == 1)
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult IsAllSeen(string friend)
        {
            string my = (string)Session["user"];

            var mesage = db.Messages.Where(x => x.ReceiverId == my && x.UsernameId == friend && x.isRead==0).OrderByDescending(x => x.Datum).ToList();
            if (mesage.Count>0)
            {
                return Json(true);
            }
            return Json(false);
        }



        [NonAction]
        private int SortInt(int i1, int i2, string sortDirection)
        {

            return sortDirection == "asc" ? i1.CompareTo(i2) : i2.CompareTo(i1);
        }

        public ActionResult LoadContactData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var cr = Request.Form.GetValues("cr").FirstOrDefault();
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();
            int cre = Int32.Parse(cr);
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var Items = (from item in db.Items select item).ToList();;
           
       
            recordsTotal =Items.Count();
            var data = (from n in Items
                        select new
                        {
                            Id = n.ID,
                            Health=n.Health,
                            Demage = n.Demage,
                            Armor=n.Armor,
                            HealthReg = n.HealthReg,
                            Image = n.Image,
                            Mana = n.Mana,
                            Price = n.Price

                        }).Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
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