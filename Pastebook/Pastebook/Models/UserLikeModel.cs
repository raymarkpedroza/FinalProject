using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserLikeModel
    {
        public USER Liker = new USER();
        public LIKE Like = new LIKE();
    }
}