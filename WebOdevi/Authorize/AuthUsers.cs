using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebOdevi.Authorize
{
    public class AuthUsers : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["usergroupname"] == "Admin")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"Controller","Admin" },
                {"Action","Index" }});
            }
            else if (filterContext.HttpContext.Session["usergroupname"] == "User")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"Controller","Home" },
                {"Action","Index" }});
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"Controller","User" },
                {"Action","Login" }});
            }
            base.OnActionExecuting(filterContext);
        }
    }
}