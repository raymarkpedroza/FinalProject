using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class AddNotificationRequest
    {
        public AddNotificationRequest()
        {
            Notification = new NotificationEntity();
        }

        [DataMember]
        public NotificationEntity Notification { get; set; }
    }
}