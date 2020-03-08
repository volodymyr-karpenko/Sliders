using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Sliders.Core.ViewModels;
using Sliders.Forms.UI.Components;

namespace Sliders.Forms.UI.Views
{
    [MvxContentPagePresentation(WrapInNavigationPage = false)]
    public partial class HomeView : MvxContentPage<HomeViewModel>, IContainSliderViews
    {
        public HomeView()
        {
            InitializeComponent();
        }

        SliderView IContainSliderViews.SliderView1 => SliderView1;
        SliderView IContainSliderViews.SliderView2 => SliderView2;
        SliderView IContainSliderViews.SliderView3 => SliderView3;
        SliderView IContainSliderViews.SliderView4 => SliderView4;
        SliderView IContainSliderViews.SliderView5 => SliderView5;
    }
}