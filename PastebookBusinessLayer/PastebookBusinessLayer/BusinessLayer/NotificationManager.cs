using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class NotificationManager
    {
        INotificationRepository _notificationRepository;

        public NotificationManager()
        {
            _notificationRepository = new NotificationRepository();
        }

        public bool CreateNotification(PASTEBOOK_NOTIFICATION notification)
        {
            return _notificationRepository.Create(notification);
        }

        public bool DeleteNotification(PASTEBOOK_NOTIFICATION notification)
        {
            return _notificationRepository.Delete(notification);
        }

        public bool UpdateNotification(PASTEBOOK_NOTIFICATION notification)
        {
            return _notificationRepository.Update(notification);
        }

        public PASTEBOOK_NOTIFICATION GetNotification(int id)
        {
            return _notificationRepository.Get(id);
        }

        public List<PASTEBOOK_NOTIFICATION> GetListOfUnseenNotification(int id)
        {
            return _notificationRepository.Find(notification => notification.RECEIVER_ID == id && notification.SEEN == "N");
        }

        public List<PASTEBOOK_NOTIFICATION> GetNotificationWithUser(Func<PASTEBOOK_NOTIFICATION, bool> predicate)
        {
            return _notificationRepository.GetNotificationWithUsers(predicate);
        }

        public PASTEBOOK_NOTIFICATION FindNotification(Func <PASTEBOOK_NOTIFICATION, bool> condition)
        {
            return _notificationRepository.Find(condition).FirstOrDefault();
        }

    }
}
