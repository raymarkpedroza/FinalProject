using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface INotificationRepository:IRepository<PASTEBOOK_NOTIFICATION>
    {
        List<PASTEBOOK_NOTIFICATION> GetNotificationWithUsers(Func<PASTEBOOK_NOTIFICATION, bool> predicate);
    }
}
