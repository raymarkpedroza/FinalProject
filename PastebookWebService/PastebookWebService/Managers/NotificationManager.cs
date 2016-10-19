using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookWebService.Managers
{
    public class NotificationManager
    {

        public int AddNotification(NotificationEntity notification)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_NOTIFICATION.Add(Mapper.MapWCFNotificationEntityToDBNotificationTable(notification));
                    result = context.SaveChanges();
                }
            }
            catch 
            {
            }

            return result;
        }

        //public List<NotificationEntity> RetrieveNotifications()
        //{
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities)
        //        {
        //            foreach (var item in collection)
        //            {

        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
    }
}