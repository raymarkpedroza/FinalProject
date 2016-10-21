using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEF;

namespace Pastebook.Models
{
    public class NotificationViewModel
    {
        public PASTEBOOK_NOTIFICATION Notification = new PASTEBOOK_NOTIFICATION();
        public List<PASTEBOOK_POST> ListOfPosts = new List<PASTEBOOK_POST>();
        public PASTEBOOK_USER Sender = new PASTEBOOK_USER();
    }
}