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

        public List<PASTEBOOK_NOTIFICATION> RetrieveNotificationById(int userId)
        {
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();
            listOfNotification = _notificationRepo.RetrieveList(x=>x.RECEIVER_ID == userId);

            return listOfNotification;
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
    }
}
