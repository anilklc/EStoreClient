using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Slider
{
    public class ResultSliderWithImages
    {
        public string Id { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubtitle { get; set; }
        public string ImagePath { get; set; }
        public string SliderTargetUrl { get; set; }
    }
}
