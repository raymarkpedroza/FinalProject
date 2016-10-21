using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookDataAccess.Managers
{
    public class DataAccessNotificationManager
    {

        public int AddNotification(PASTEBOOK_NOTIFICATION notification)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_NOTIFICATION.Add(notification);
                    result = context.SaveChanges();
                }
            }

            catch 
            {
            }

            return result;
        }

        public List<PASTEBOOK_NOTIFICATION> RetrieveNotifications(int receiverId)
        {
            List<PASTEBOOK_NOTIFICATION> listOfNotification = new List<PASTEBOOK_NOTIFICATION>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfNotification = context.PASTEBOOK_NOTIFICATION.Where(x=>x.RECEIVER_ID == receiverId).ToList();
                }
            }
            catch
            {

            }

            return listOfNotification;
        }
    }
}