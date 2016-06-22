using System;
using System.Collections.Generic;

namespace Intouch.Core
{
    public class WifiSession
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual Point Point { get; set; }
        public TimeSpan Duration { get; set; }
        public WiFiSessionType Type { get; set; }
        public DateTimeOffset? AccessGranted { get; set; }
        public DateTimeOffset? Closed { get; set; }
        public WiFiSessionClosedReason ClosedReason { get; set; }

    }
    public enum WiFiSessionType
    {
        Default,
        Vip
    }

    public enum WiFiSessionClosedReason
    {
        Logout,
        SessionTimeout,
        LoggedFromAnotherPoint,
        SessionAborted
    }
}