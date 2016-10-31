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
            //var authorized = base.AuthorizeCore(httpContext);

            //if (!authorized)
            //{
            //    //not authenticated or form authentication expired
            //    return false;
            //}

            //now check the session
            var user = httpContext.Session["UserId"];
            if (user == null)
            {
                //session expired
                return false;
            }

            return true;
        }
    }
}