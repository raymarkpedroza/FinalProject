using Pastebook.Managers;
using Pastebook.Models;
using PastebookDataAccess.Managers;
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
        DataAccessNotificationManager daNotificationManager = new DataAccessNotificationManager();
        public JsonResult AddNotification(int id, string notificationType, int receiverId)
        {
            PASTEBOOK_NOTIFICATION notification = new PASTEBOOK_NOTIFICATION();

            if (!string.IsNullOrEmpty(notificationType))
            {
                notification.NOTIF_TYPE = notificationType;

                if (notificationType == "L")
                {
                    notification.POST_ID = id;
                }

                else if (notificationType == "C")
                {
                    notification.COMMENT_ID = id;
                }
            }

            notification.RECEIVER_ID = receiverId;
            notification.SEEN = "N";
            notification.SENDER_ID = (int)Session["UserId"];
            notification.CREATED_DATE = DateTime.Now;

            int result = 0;
            result = daNotificationManager.AddNotification(notification);

            return Json(new { result = result });
        }
    }
}