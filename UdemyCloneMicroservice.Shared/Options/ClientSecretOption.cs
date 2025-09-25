using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Shared.Options
{
    public class ClientSecretOption
    {
        public required string Id { get; set; }
        public required string Secret { get; set; }
    }
}