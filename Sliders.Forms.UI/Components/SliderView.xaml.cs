using Xamarin.Forms;

namespace Sliders.Forms.UI.Components
{
    public partial class SliderView : ContentView
    {
        public SliderView()
        {
            InitializeComponent();

            Slider = SliderIcon;
            Indicator = IndicatorLabel;
        }

        public View Slider { get; set; }
        public Label Indicator { get; set; }
    }
}