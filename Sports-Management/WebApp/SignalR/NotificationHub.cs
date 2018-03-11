using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using WebApp.HelperClass;

namespace WebApp.SignalR
{
    public class NotificationHub : Hub
    {
        static List<UserConnection> uList = new List<UserConnection>();
        public static void SendNotification(List<long> users,string text, string icon, string url,string image)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            List<string> usersList = uList.Where(w => users.Contains(w.UserId)).Select(s => s.ConnectionID).ToList();
            context.Clients.Clients(usersList).broadcastMessage(text, icon, url, image);
        }

        public override Task OnConnected()
        {
            //Get the UserId
            var us = new UserConnection();
            us.UserId = Common.CurrentUser.Id;
            us.ConnectionID = Context.ConnectionId;
            uList.Add(us);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            uList.RemoveAt(uList.FindIndex(u => u.ConnectionID == Context.ConnectionId));
            return base.OnDisconnected(stopCalled);
        }
    }

    class UserConnection
    {
        public long UserId { set; get; }
        public string ConnectionID { set; get; }
    }
}