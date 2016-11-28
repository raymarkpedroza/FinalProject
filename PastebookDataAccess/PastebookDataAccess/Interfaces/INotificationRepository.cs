using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface INotificationRepository:IRepository<NOTIFICATION>
    {
        List<NOTIFICATION> GetNotificationWithUsers(Func<NOTIFICATION, bool> predicate);
    }
}
