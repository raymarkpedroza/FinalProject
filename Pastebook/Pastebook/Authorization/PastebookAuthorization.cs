using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Pastebook
{
    public class PastebookAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.Session["UserId"];
            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}