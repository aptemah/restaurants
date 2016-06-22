using System;
using System.Collections.Generic;

namespace Intouch.Core
{
    public class Network
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NetworkBranch Branch { get; set; }
        public virtual ICollection<Point> Points { get; set; }
        public bool PersonalizationEnable { get; set; }
        public string WelcomePage { get; set; }
        public virtual ICollection<RestNetwork> RestNetworks { get; set; }

        public string BranchName
        {
            get
            {
                switch (Branch)
                {
                    case NetworkBranch.Karaoke:
                        return "караоке-клубе";
                    case NetworkBranch.Fitness:
                        return "фитнес-клубе";
                    case NetworkBranch.Restaurant:
                        return "ресторане";
                }
                return null;
            }
        }
    }


    public enum NetworkBranch
    {
        Fitness,
        Karaoke,
        Restaurant
    }
}