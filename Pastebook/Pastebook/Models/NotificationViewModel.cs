using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEF;

namespace Pastebook.Models
{
    public class NotificationViewModel
    {
        public NOTIFICATION Notification = new NOTIFICATION();
        public List<POST> ListOfPosts = new List<POST>();
        public USER Sender = new USER();
    }
}