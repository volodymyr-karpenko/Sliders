using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Sliders.Core.Models;
using Sliders.Forms.UI.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sliders.Forms.UI.Behaviors
{
    public class SlidersDataPresentBehavior : Behavior
    {
        private IContainSliderViews _sliderViews;
        private IMvxMessenger _messenger;
        private MvxSubscriptionToken _token;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            if (bindable != null)
            {
                _sliderViews = bindable as IContainSliderViews;
                _messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
                _token = _messenger.SubscribeOnMainThread<SlidersDataMessage>(PresentData);
            }
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            if (_token != null)
            {
                _token.Dispose();
            }
            _sliderViews = null;
            _messenger = null;
            base.OnDetachingFrom(bindable);
        }

        private void PresentData(SlidersDataMessage args)
        {
            if (_messenger == null)
            {
                return;
            }
            Task task = PresentDataAsync(args.Data);
            task.ContinueWith(t =>
            {
                _messenger.Publish(new ReadDataMessage(this));
            });
        }

        private async Task PresentDataAsync(SlidersData data)
        {
            if (data == null || _sliderViews == null)
            {
                return;
            }

            _sliderViews.SliderView1.Indicator.Text = data.Slider1.ToString();
            _sliderViews.SliderView2.Indicator.Text = data.Slider2.ToString();
            _sliderViews.SliderView3.Indicator.Text = data.Slider3.ToString();
            _sliderViews.SliderView4.Indicator.Text = data.Slider4.ToString();
            _sliderViews.SliderView5.Indicator.Text = data.Slider5.ToString();

            await Task.WhenAll(
                _sliderViews.SliderView1.Slider.TranslateTo(0, data.Slider1 * -1, 1000, Easing.Linear),
                _sliderViews.SliderView2.Slider.TranslateTo(0, data.Slider2 * -1, 1000, Easing.Linear),
                _sliderViews.SliderView3.Slider.TranslateTo(0, data.Slider3 * -1, 1000, Easing.Linear),
                _sliderViews.SliderView4.Slider.TranslateTo(0, data.Slider4 * -1, 1000, Easing.Linear),
                _sliderViews.SliderView5.Slider.TranslateTo(0, data.Slider5 * -1, 1000, Easing.Linear)
            );
        }
    }
}