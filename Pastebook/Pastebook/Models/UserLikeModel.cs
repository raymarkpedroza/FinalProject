using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserLikeModel
    {
        public PASTEBOOK_USER Liker = new PASTEBOOK_USER();
        public PASTEBOOK_LIKE Like = new PASTEBOOK_LIKE();
    }
}