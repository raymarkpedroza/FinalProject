using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class NotificationRepository : Repository<NOTIFICATION>, INotificationRepository
    {
        public List<NOTIFICATION> GetNotificationWithUsers(Func<NOTIFICATION, bool> predicate)
        {
            using (var context = new PastebookEntities())
            {
                return context.NOTIFICATIONs
                    .Include(notification => notification.POST)
                    .Include(notification => notification.USER)
                    .Include(notification => notification.USER1)
                    .Include(notification => notification.COMMENT)
                    .Where(predicate)
                    .OrderByDescending(x => x.CREATED_DATE)
                    .ToList();
            }
        }
    }
}
