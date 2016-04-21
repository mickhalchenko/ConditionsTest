using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExspressionTest
{
    public class UserAgentViewModel
    {
        public string OperatingSystem { get; set; }
        public string OperatingSystemVersion { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string OperationSystemBitness { get; set; }

        public static UserAgentViewModel DefaultAgentViewModel()
        {
            return  new UserAgentViewModel()
            {
                OperatingSystem = "Windows",
                OperatingSystemVersion = "10",
                BrowserName = "Chrome",
                BrowserVersion = "48",
                OperationSystemBitness = "64"
            };
        }
 }
 
}
