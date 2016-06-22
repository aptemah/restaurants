using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.SocialNetwork
{
    public class MessagesComparer : IEqualityComparer<MessagesModel>
    {

        public bool Equals(MessagesModel x, MessagesModel y)
        {
            return x.User.Id == y.User.Id && x.User.SocId == y.User.SocId;
        }

        public int GetHashCode(MessagesModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            return obj.GetHashCode();
        }
    }
}