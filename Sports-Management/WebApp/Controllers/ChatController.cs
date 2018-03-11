using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.HelperClass;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        INotificationsService _notificationService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public ChatController(INotificationsService notificationService, IUnitOfWorkAsync unitOfWork)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNotification()
        {
            if (Request.IsAjaxRequest())
            {
                var notifications = _notificationService.GetNotificationByUserId(Common.CurrentUser.Id).Select(s => new
                {
                    IsRead = s.NotificationDate < DateTime.Now.Date ? true : s.IsRead,
                    Notification = s.Notification,
                    NotificationId = s.NotificationId,
                    Icon = s.Icon,
                    Link = s.Link,
                    User = s.Users,
                    NotificationDate = s.NotificationDate,
                    NotificationDateString = s.NotificationDate.ToShortDateString(),
                    UserId = s.UserId,
                    ProfilePic=s.ProfilePic
                }).OrderByDescending(o => o.NotificationId).Take(15).ToList();
                return Json(new { success = true, notifications = notifications, count = notifications.Where(w => w.IsRead == false && w.NotificationDate.Date == DateTime.Now.Date).Count() }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult MarkReadGetNotification(long id)
        {
            var notification = _notificationService.Find(id).data;
            notification.ObjectState = ObjectState.Modified;
            notification.IsRead = true;
            _notificationService.InsertOrUpdateGraph(notification);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = notification.Link }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
    }
}