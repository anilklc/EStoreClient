using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Slider
{
    public class CreateSlider
    {
        public string SliderTitle { get; set; }
        public string SliderSubtitle { get; set; }
        public string SliderTargetUrl { get; set; }
        public bool SliderActive { get; set; }
    }
}
