using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserCommentModel
    {
        public PASTEBOOK_USER Commenter = new PASTEBOOK_USER();
        public PASTEBOOK_COMMENT Comment = new PASTEBOOK_COMMENT();
    }
}