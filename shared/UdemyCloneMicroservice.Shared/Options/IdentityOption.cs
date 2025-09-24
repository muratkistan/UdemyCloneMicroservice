using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Shared.Options
{
    public class IdentityOption
    {
        public required string Address { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}