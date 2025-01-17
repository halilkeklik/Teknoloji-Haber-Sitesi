﻿using System;
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
            if (HttpContext.Current.Session["usergroupname"] != null)
            {
                if (HttpContext.Current.Session["usergroupname"].ToString() != "Admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"Controller","User" },
                {"Action","Login" }});
                }
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