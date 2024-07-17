using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.OrderStatus
{
    public class OrderStatusList
    {
        public List<string> Status = new List<string>()
        {
                    "Placed",
                    "Preparing",
                    "Shipped",
                    "Completed",
                    "Cancelled",
        };
    }
}
