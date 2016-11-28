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

        public bool CreateNotification(NOTIFICATION notification)
        {
            return _notificationRepository.Create(notification);
        }

        public bool DeleteNotification(NOTIFICATION notification)
        {
            return _notificationRepository.Delete(notification);
        }

        public bool UpdateNotification(NOTIFICATION notification)
        {
            return _notificationRepository.Update(notification);
        }

        public NOTIFICATION GetNotification(int id)
        {
            return _notificationRepository.Get(id);
        }

        public List<NOTIFICATION> GetListOfUnseenNotification(int id)
        {
            return _notificationRepository.Find(notification => notification.RECEIVER_ID == id && notification.SEEN == "N");
        }

        public List<NOTIFICATION> GetNotificationWithUser(Func<NOTIFICATION, bool> predicate)
        {
            return _notificationRepository.GetNotificationWithUsers(predicate);
        }

        public NOTIFICATION FindNotification(Func <NOTIFICATION, bool> condition)
        {
            return _notificationRepository.Find(condition).FirstOrDefault();
        }

    }
}
