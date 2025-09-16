using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Order.Domain.Entities
{
    public class BaseEntity<TEntityId>
    {
        public TEntityId Id { get; set; } = default!;
    }
}