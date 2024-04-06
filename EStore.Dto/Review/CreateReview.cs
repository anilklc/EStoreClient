using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Review
{
    public class CreateReview
    {
        public string ReviewerName { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewComment { get; set; }
    }
}
