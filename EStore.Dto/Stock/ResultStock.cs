using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Stock
{
    public class ResultStock
    {
        public string Id { get; set; }
        public string ProductSize { get; set; }
        public int ProductStock { get; set; }
        public Guid ProductId { get; set; }
    }
}
