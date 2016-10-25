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
        bool DeleteNotification(PASTEBOOK_NOTIFICATION notification);

        PASTEBOOK_NOTIFICATION RetrieveNotificationByNotificationId(int notificationId);
        PASTEBOOK_NOTIFICATION RetrieveCommentNotificationByCommentIdAndUserId(int commentId, int userId);
        PASTEBOOK_NOTIFICATION RetrieveLikeNotificationByPostIdAndUserId(int postId, int userId);
        List<PASTEBOOK_NOTIFICATION> RetrieveNotificationByUserId(int userId);

    }
}
