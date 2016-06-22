using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Intouch.Core
{
    public class CORSActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "OPTIONS")
            {
                // do nothing let IIS deal with reply!
                filterContext.Result = new EmptyResult();
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}