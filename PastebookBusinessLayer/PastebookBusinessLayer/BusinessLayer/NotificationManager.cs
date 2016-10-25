using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess.Repositories;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class NotificationManager : INotificationManager
    {
        INotificationRepository _notificationRepo;

        public NotificationManager()
        {
            _notificationRepo = new NotificationRepository();
        }

        public List<PASTEBOOK_NOTIFICATION> RetrieveNotificationByUserId(int userId)
        {
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();
            listOfNotification = _notificationRepo.RetrieveList(x=>x.RECEIVER_ID == userId, sender=>sender.PASTEBOOK_USER1, post=>post.PASTEBOOK_POST);

            return listOfNotification;
        }

        public PASTEBOOK_NOTIFICATION RetrieveNotificationByNotificationId(int notificationId)
        {
            PASTEBOOK_NOTIFICATION notification = new PASTEBOOK_NOTIFICATION();
            notification = _notificationRepo.RetrieveSpecificRecord(x => x.RECEIVER_ID == notificationId, sender => sender.PASTEBOOK_USER1, post => post.PASTEBOOK_POST);

            return notification;
        }

        public bool CreateNotification(PASTEBOOK_NOTIFICATION notification)
        {
            bool result = false;
            result = _notificationRepo.CreateRecord(notification);

            return result;
        }

        public bool UpdateNotification(PASTEBOOK_NOTIFICATION notification)
        {
            bool result = false;
            result = _notificationRepo.UpdateRecord(notification);

            return result;
        }

        public bool DeleteNotification(PASTEBOOK_NOTIFICATION notification)
        {
            bool result = false;
            result = _notificationRepo.DeleteRecord(notification);

            return result;
        }

        public PASTEBOOK_NOTIFICATION RetrieveCommentNotificationByCommentIdAndUserId(int commentId, int userId)
        {
            PASTEBOOK_NOTIFICATION notification = new PASTEBOOK_NOTIFICATION();
            notification = _notificationRepo.RetrieveList(x => x.COMMENT_ID == commentId, sender => sender.PASTEBOOK_USER1, post => post.PASTEBOOK_POST).Where(x=>x.SENDER_ID == userId).FirstOrDefault();

            return notification;
        }

        public PASTEBOOK_NOTIFICATION RetrieveLikeNotificationByPostIdAndUserId(int postId, int userId)
        {
            PASTEBOOK_NOTIFICATION notification = new PASTEBOOK_NOTIFICATION();
            notification = _notificationRepo.RetrieveList(x => x.POST_ID == postId, sender => sender.PASTEBOOK_USER1, post => post.PASTEBOOK_POST).Where(x => x.SENDER_ID == userId).FirstOrDefault();

            return notification;
        }
    }
}
