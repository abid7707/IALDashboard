using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IALDashboard.Filters
{
    public class AuthorizedUser : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["isLogedin"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        {"Controller","User"},
                        {"Action","Login"}
                    });

            }
            base.OnActionExecuting(filterContext);
        }
    }
}