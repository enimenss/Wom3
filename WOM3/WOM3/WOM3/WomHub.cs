using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WOM3.DAL;
using System.Web.Script.Serialization;
using System.Data.Entity.Migrations;
using WOM3.Models;
using System.Threading.Tasks;

namespace WOM3
{
    [HubName("WomHub")]
    public class WomHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = ConnectionMapping<string>.GetConection;

        private WOMContext db = new WOMContext();


        public void Send(string naslov,string poruka)
        {
            News novost = new Models.News {Naslov=naslov, Info = poruka, Datum = DateTime.Now };
            db.News.Add(novost);
            db.SaveChanges();
            var users = from s in db.Users select s;
            foreach (var user in users)
            {
                int number = user.NumOfNews + 1;
                db.Users.Attach(user);
                db.Entry(user).Property(x => x.NumOfNews).CurrentValue =number;

               

                foreach (var connectionId in _connections.GetConnections(user.Username))
                {
                    Clients.Client(connectionId).News(naslov,poruka, DateTime.Now.ToString(),number);

                }

            }
            db.SaveChanges();

        }

        public void af(string frend)
        {
            string name = Context.QueryString["Name"].ToString();
            User user = db.Users.Find(name);
            User myfrend = db.Users.Find(frend);
 
            FriendRequests remove = db.FriendRequests.Where(x => x.UsernameId == name && x.FriendRId == frend).FirstOrDefault();
            db.FriendRequests.Remove(remove);
            db.SaveChanges();
            DateTime date = DateTime.Now;
            Friends friends1 = new Friends { UsernameId = name, FriendId = frend, Datum = date };
            Friends friends2 = new Friends { UsernameId = frend, FriendId = name ,Datum = date };
            db.Friends.Add(friends1);
            db.SaveChanges();
            db.Friends.Add(friends2);
            db.SaveChanges();
            int num = db.FriendRequests.Where(f => f.UsernameId == name).Count();
            foreach (var connectionId in _connections.GetConnections(frend))
            {
                Clients.Client(connectionId).Prihvacen(user.Username,user.Avatar);

            }
            Clients.Caller.Prihvacen(frend,myfrend.Avatar);
            Clients.Caller.Notify(num);


        }

        public void df(string frend)
        {
            string name = Context.QueryString["Name"].ToString();
            FriendRequests remove = db.FriendRequests.Where(x => x.UsernameId == name && x.FriendRId == frend).FirstOrDefault();
            db.FriendRequests.Remove(remove);
            db.SaveChanges();
            int num = db.FriendRequests.Where(f => f.UsernameId ==name).Count();
            foreach (var connectionId in _connections.GetConnections(frend))
            {
                Clients.Client(connectionId).Odbijen(name);

            }
            Clients.Caller.Notify(num);

        }
        public void friendRequest(string username,string target)
        {
           int count= db.FriendRequests.Where(f => f.UsernameId == target && f.FriendRId == username).Count();
            if (count == 0)
            {
                FriendRequests friendRequest = new FriendRequests { UsernameId = target, FriendRId = username, Datum = DateTime.Now };
                db.FriendRequests.Add(friendRequest);
                db.SaveChanges();
                int num = db.FriendRequests.Where(f => f.UsernameId == target).Count();


                foreach (var connectionId in _connections.GetConnections(target))
                {
                    Clients.Client(connectionId).Notify(num);

                }
            }

        }

        public void offlineSam()
        {
            string name = Context.QueryString["Name"].ToString();
            User user = db.Users.Find(name);

            var s = from p in db.Friends where (p.UsernameId == name) select p.FriendId;
            foreach (var username in s)
            {
                foreach (var connectionId in _connections.GetConnections(username))
                {
                    Clients.Client(connectionId).offSam(user.Username, user.Avatar);

                }
            }

        }


        public void posaljiPoruku(string mesage,string target)
        {
            string name = Context.QueryString["Name"].ToString();
            foreach (var connectionId in _connections.GetConnections(target))
            {
                Clients.Client(connectionId).primiPoruku(mesage, name,DateTime.Now.ToString());

            }
            Clients.Caller.mojaPoruka(mesage,target,DateTime.Now.ToString());

            Messages poruka = new Messages() { UsernameId = name, ReceiverId = target, Datum = DateTime.Now,Message=mesage,isRead=1 };
            db.Messages.Add(poruka);
            db.SaveChanges();
           

        }



        public override Task OnConnected()
        {
            string name = Context.QueryString["Name"].ToString();

            User user = db.Users.Find(name);

            var s = from p in db.Friends where (p.UsernameId == name) select p.FriendId;
            foreach (var username in s)
            {
                foreach (var connectionId in _connections.GetConnections(username))
                {
                    Clients.Client(connectionId).onlineSam(user.Username,user.Avatar);

                }
            }

            _connections.Add(name, Context.ConnectionId);
            


            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.QueryString["Name"].ToString();

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.QueryString["Name"].ToString();

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
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