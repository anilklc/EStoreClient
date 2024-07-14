using EStore.Dto.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Order
{
    public class CreateOrder
    {
        public string GrandTotal { get; set; }
        public string CartTotal { get; set; }
        public List<CartItemList> CartItems { get; set; }
        
    }
}
