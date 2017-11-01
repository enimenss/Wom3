using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WOM3.DAL;
using WOM3.MD5Hash;
using WOM3.Models;
using WOM3.SendMail;

namespace WOM3.Controllers
{
    public class WelcomeController : Controller
    {
        private WOMContext db = new WOMContext();
       
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Profiles/" + Session["user"], "Home");


            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include="Email")] EmailReg reg,FormCollection LogParam)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(u => u.Email == reg.Email).FirstOrDefault();
                if (user != null)
                {
                    ViewBag.Error = "Postoji korisnik sa mailom";
                    return View();
                }
                Mail.SendMail(reg.Email, "<a href=http://localhost:5611/Welcome/Register/" + reg.Email+">Aktiviraj nalog </a>");
                EmailReg check = db.EmailRegs.Find(reg.Email);
                if (check == null)
                {
                    db.EmailRegs.Add(reg);
                    db.SaveChanges();
                   
                }
                ViewBag.Errorr = "MailPoslat";
                return View();

            }
            
            if(LogParam["Username"]!=null && LogParam["Pass"] != null)
            {
                string username = (string)LogParam["Username"];
                string pass = (string)LogParam["Pass"];
                string hash = pass;
               // string hash = MD5.CreateMD5(pass);
                


                User user = db.Users.Where(b => b.Username == username && b.Pass == hash).FirstOrDefault();
                if (user != null)
                {
                    Session["user"] = user.Username;
                    if (!string.Equals(user.Username, "Administrator"))
                        return RedirectToAction("Profiles/" + username, "Home");
                    
                    return RedirectToAction("PostNews", "Home");
                }
                else
                {
                    ViewBag.Incorect = "Korisnicko ime ili sifra nisu dobri";
                    return View();
                }

            }
            return View();
        }

        public ActionResult Register(string id)
        {
            EmailReg check = db.EmailRegs.Find(id);
            if (check == null)
            {
                return RedirectToAction("Index", "Welcome");

            }
            User user = new Models.User() { Email = id };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username,Email,Pass,Avatar")] User user)
        {
           
            if (ModelState.IsValid)
            {
                User check=db.Users.Find(user.Username);
                if (check != null)
                {
                    ViewBag.Error = "postojeci User";
                    ViewBag.Email = user.Email;
                    return View(user);
                }


               string hash = MD5.CreateMD5(user.Pass);
                 user.Pass = hash;
                db.Users.Add(user);
                db.SaveChanges();

                UserStats stats = new UserStats { Username = user.Username };
                UserHeroes heros = new UserHeroes { Username = user.Username, Name = "mage04", Wins = 0, Loses = 0 };
                db.UserHeroes.Add(heros);
                db.SaveChanges();
                heros.Name = "mage01";
                db.UserHeroes.Add(heros);
                db.SaveChanges();
                heros.Name = "mage02";
                db.UserHeroes.Add(heros);
                db.SaveChanges();
                heros.Name = "mage03";
                db.UserHeroes.Add(heros);
                db.SaveChanges();

                db.UserStats.Add(stats);
                Session["user"] = user.Username;
               
                EmailReg remove = new EmailReg { Email = user.Email };
                db.EmailRegs.Attach(remove);
                db.EmailRegs.Remove(remove);
                db.SaveChanges();
                return RedirectToAction("Profiles/" + user.Username, "Home");
            }

         
            return View(user);
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