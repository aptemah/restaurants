using Intouch.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Intouch.Restaurant
{
    public class ImageServices
    {
        protected CoreContext db = new CoreContext();
        public static string ContentPath(string folderName)
        {
            var ContentPath = Path.Combine(new DirectoryInfo(HttpContext.Current.Server.MapPath("~") +
                Config.ContentAvatarPath).ToString(), folderName);
            return ContentPath;
        }
    }
}