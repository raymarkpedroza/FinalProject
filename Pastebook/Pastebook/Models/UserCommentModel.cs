﻿using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserCommentModel
    {
        public USER Commenter = new USER();
        public COMMENT Comment = new COMMENT();
    }
}