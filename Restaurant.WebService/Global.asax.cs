using Intouch.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Intouch.Restaurant
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new CoreContextSeedInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
        }
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    EnableCrossDmainAjaxCall();
        //}
        //private void EnableCrossDmainAjaxCall()
        //{
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

        //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //    {
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");

        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");

        //        HttpContext.Current.Response.End();
        //    }
        //}
    }
}
