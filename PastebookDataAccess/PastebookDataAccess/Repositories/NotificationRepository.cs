using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class NotificationRepository : Repository<PASTEBOOK_NOTIFICATION>, INotificationRepository
    {
        public List<PASTEBOOK_NOTIFICATION> GetNotificationWithUsers(Func<PASTEBOOK_NOTIFICATION, bool> predicate)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_NOTIFICATION
                    .Include(notification => notification.PASTEBOOK_POST)
                    .Include(notification => notification.PASTEBOOK_USER)
                    .Include(notification => notification.PASTEBOOK_USER1)
                    .Include(notification => notification.PASTEBOOK_COMMENT)
                    .Where(predicate)
                    .OrderByDescending(x => x.CREATED_DATE)
                    .ToList();
            }
        }
    }
}
