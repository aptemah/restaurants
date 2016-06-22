using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Utils.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public JsonResult Test()
        {
            return Json("test", JsonRequestBehavior.AllowGet);
        }
    }
}