using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Bus.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid UserId);
}