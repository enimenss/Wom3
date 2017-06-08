using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WOM3.DAL;
using WOM3.Models;

namespace WOM3.Controllers
{
    public class Users1Controller : ApiController
    {
        private WOMContext db = new WOMContext();

        //API func for Login
        [HttpPost]
        public IHttpActionResult Login([FromBody]logUser user)
        {
            // return Ok(id);
            User u = db.Users.Find(user.userName);
            if (u == null )
            {
                return NotFound();
            }
            if (!u.Pass.Equals(user.hashPassword))
                return BadRequest("wrong password!!");
           APIModels.Token n = new APIModels.Token { token = Environment.TickCount.ToString(), Username = user.userName};
            try
            {
                db.Tokens.Add(new Token { Username=n.Username,token=n.token,User=u});
                db.SaveChanges();
            }
            catch (Exception g)
            {
                return BadRequest();
            }
            return Ok(n);
        }
       [HttpPost]
        public IHttpActionResult SetResult([FromBody]APIModels.GameResult res)
        {
           
            Token u = db.Tokens.Find(res.Token);
            if (u == null)
            {
                return NotFound();
            }
            UserStats s = db.UserStats.Find(u.Username);
            try
            {
                int points = s.Points + (int)res.demage;
                int gold = s.Gold + res.kils * 10;
                int win = s.Wins;
                int lose = s.Loses;
                if (res.win)
                {
                    win++;
                }
                else
                {
                    lose++;
                }
                db.Entry(s).Property(x => x.Points).CurrentValue = points;
                db.Entry(s).Property(x => x.Gold).CurrentValue = gold;
                db.Entry(s).Property(x => x.Wins).CurrentValue = win;
                db.Entry(s).Property(x => x.Loses).CurrentValue = lose;
                db.SaveChanges();
            }
            catch (Exception g)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        //Logout id=Token
        [HttpGet]
        public IHttpActionResult Logout(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return NotFound();
            }
            try
            {
                db.Tokens.Remove(u);
                db.SaveChanges();
            }
            catch (Exception g)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return NotFound();
            }
            User n = db.Users.Find(u.Username);
            return Ok(new APIModels.User { Username=n.Username,Email=n.Email,Avatar=n.Avatar});
        }
        [HttpGet]
        public IHttpActionResult GetUserStat(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return NotFound();
            }
            UserStats n = db.UserStats.Find(u.Username);
            return Ok(new APIModels.UserStats { Wins=n.Wins,Loses = n.Loses,Points =n.Points,Gold = n.Gold });
        }
        [HttpGet]
        public IHttpActionResult GetUserItems(string id)
        {
            Token u = db.Tokens.Find(id);
            if (u == null)
            {
                return NotFound();
            }
            User n = db.Users.Find(u.Username);
            APIModels.ListOfItems list = new APIModels.ListOfItems();
            List<UserItems> l = db.UserItems.Where(x => x.Username == n.Username).Include(x=>x.Item).ToList();
            list.list = new List<APIModels.Items>();
            for(int i=0;i<l.Count;i++)
            {
                list.list.Add(new APIModels.Items { ID = l[i].Item.ID, Image=l[i].Item.Image, Health = l[i].Item.Health, Mana = l[i].Item.Mana, HealthReg = l[i].Item.HealthReg, ManaReg = l[i].Item.ManaReg, Armor = l[i].Item.Armor, Demage = l[i].Item.Demage, Price = l[i].Item.Price });
            }
            return Ok(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Username == id) > 0;
        }
    }
}