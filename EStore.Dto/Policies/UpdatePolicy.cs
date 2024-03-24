using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Policies
{
    public class UpdatePolicy
    {
        public string Id { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyIcon { get; set; }
        public string PolicyTabHref { get; set; }
    }
}
