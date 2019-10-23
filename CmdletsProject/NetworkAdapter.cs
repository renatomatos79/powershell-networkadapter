using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace CmdletsProject
{
    public class NetworkAdapter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeviceId { get; set; }
        public string Manufacturer { get; set; }
        public string NetConnectionId { get; set; }
        public bool PhysicalAdapter { get; set; }
        public int MaxEntries { get; set; } = 100;
    }
}
