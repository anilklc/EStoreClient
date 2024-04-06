namespace EStore.Dto.Slider
{
    public class UpdateSlider
    {
        public string Id { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubtitle { get; set; }
        public string? SliderImagePath { get; set; }
        public string SliderTargetUrl { get; set; }
        public bool SliderActive { get; set; }
    }
}
