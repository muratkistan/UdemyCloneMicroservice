using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Bus
{
    public class BusOption
    {
        public required string Address { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required int Port { get; set; }
    }
}