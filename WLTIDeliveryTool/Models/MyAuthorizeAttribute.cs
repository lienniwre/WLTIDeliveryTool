using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WLTIDeliveryTool.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Forbidden");
            }
            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.Request.IsAuthenticated){
                if (base.Roles != "")
                {
                    var userModel = new UserModel();
                    var session = userModel.GetSession();
                    var role = session.Role;

                    if (base.Roles.Contains(role))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}