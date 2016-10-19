using Pastebook.Managers;
using Pastebook.Models;
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
        public JsonResult AddNotification(int id, string notificationType, int receiverId)
        {
            NotificationModel notification = new NotificationModel();

            if (!string.IsNullOrEmpty(notificationType))
            {
                notification.NotificationType = notificationType;

                if (notificationType == "L")
                {
                    notification.PostId = id;
                }

                else if (notificationType == "C")
                {
                    notification.CommentId = id;
                }
            }

            notification.ReceiverId = receiverId;
            notification.Seen = 'N';
            notification.SenderId = (int)Session["UserId"];
            notification.CreatedDate = DateTime.Now;

            int result = 0;
            result = notificationManager.AddNotification(notification);

            return Json(new { result = result });
        }
    }
}