using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Intouch.Restaurant
{
    public class Config
    {
        public static string ContentAvatarPath
        {
            get { return WebConfigurationManager.AppSettings["ContentAvatarPath"]; }
        }
    }
}