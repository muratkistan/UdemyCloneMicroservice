using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Order.Domain.Entities
{
    public enum OrderStatus
    {
        WaitingForPayment = 1,
        Paid = 2,
        Cancel = 3,
    }
}