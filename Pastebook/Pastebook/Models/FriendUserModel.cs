using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class FriendUserModel
    {
        public PASTEBOOK_FRIEND Friend = new PASTEBOOK_FRIEND();
        public PASTEBOOK_USER FriendDetails = new PASTEBOOK_USER();
    }
}