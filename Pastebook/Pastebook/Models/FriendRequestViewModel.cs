using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class FriendRequestViewModel
    {
        public PASTEBOOK_FRIEND FriendRequest = new PASTEBOOK_FRIEND();
        public PASTEBOOK_USER Requester = new PASTEBOOK_USER();
    }
}