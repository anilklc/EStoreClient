using EStore.Dto.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Order
{
    public class CreateOrder
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string AddressId { get; set; }
        public string OrderStatus { get; set; }
        public string CargoTracking { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
    }
}
