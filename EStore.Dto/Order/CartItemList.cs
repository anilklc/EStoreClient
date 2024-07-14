using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Order
{
    public class CartItemList
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCoverImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotal { get; set; }

    }
}
