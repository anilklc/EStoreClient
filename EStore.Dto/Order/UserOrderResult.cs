using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Order
{
    public class UserOrderResult
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string AddressId { get; set; }
        public string OrderStatus { get; set; } = String.Empty;
        public string CargoTracking { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }

    }
}
