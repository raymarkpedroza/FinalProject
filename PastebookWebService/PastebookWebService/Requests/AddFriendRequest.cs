﻿using PastebookWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PastebookWebService.Requests
{
    [DataContract]
    public class AddFriendRequest
    {
        public AddFriendRequest()
        {
            Friend = new FriendEntity();
        }
        [DataMember]
        public FriendEntity Friend { get; set; }
          
    }
}