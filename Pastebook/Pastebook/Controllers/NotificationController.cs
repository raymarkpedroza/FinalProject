using Pastebook.Models;
using PastebookBusinessLayer.BusinessLayer;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager notificationManager = new NotificationManager();

        public PartialViewResult GetNotifications()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<NOTIFICATION> listOfNotification = new List<NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"] && notification.SENDER_ID != (int)Session["UserId"]);

            return PartialView("~/Views/Pastebook/_NotificationPartialView.cshtml", listOfNotification);
        }

        public PartialViewResult GetNotificationItems()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<NOTIFICATION> listOfNotification = new List<NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"]);

            return PartialView("~/Views/Pastebook/_NotificationItemPartialView.cshtml", listOfNotification.ToList());

        }

        public JsonResult SawNotification()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<NOTIFICATION> listOfNotification = new List<NOTIFICATION>();

            listOfNotification = notificationManager.GetListOfUnseenNotification((int)Session["UserId"]);

            bool result = false;

            foreach (var notification in listOfNotification)
            {
                notification.SEEN = "Y";
                result = notificationManager.UpdateNotification(notification);
            }

            return Json(new { result = result });
        }


        [HttpGet, Route("notification/viewallnotification")]
        public ActionResult Notification()
        {
            List<NotificationViewModel> listOfNotificationWithSender = new List<NotificationViewModel>();
            List<NOTIFICATION> listOfNotification = new List<NOTIFICATION>();

            listOfNotification = notificationManager.GetNotificationWithUser(notification => notification.RECEIVER_ID == (int)Session["UserId"]);

            return View("~/Views/Pastebook/Notifications.cshtml", listOfNotification.ToList());
        }

        public JsonResult GetNotificationsCount()
        {
            int notifCount = 0;

            if (Session != null && Session["UserId"] != null)
            {
                notifCount = notificationManager.GetListOfUnseenNotification((int)Session["UserId"]).Count();
            }

            return Json(new { result = notifCount }, JsonRequestBehavior.AllowGet);
        }
    }
}