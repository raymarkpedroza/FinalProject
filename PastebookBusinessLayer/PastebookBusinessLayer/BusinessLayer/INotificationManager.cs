using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public interface INotificationManager
    {
        bool CreateNotification(PASTEBOOK_NOTIFICATION notification);
        bool UpdateNotification(PASTEBOOK_NOTIFICATION notification);

        List<PASTEBOOK_NOTIFICATION> RetrieveNotificationById(int userId);

    }
}
