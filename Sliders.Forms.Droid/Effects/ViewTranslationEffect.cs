using Android.Animation;
using Sliders.Forms.Droid.Effects;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Sliders")]
[assembly: ExportEffect(typeof(ViewTranslationEffect), nameof(ViewTranslationEffect))]

namespace Sliders.Forms.Droid.Effects
{
    public class ViewTranslationEffect : PlatformEffect
    {
        private Android.Views.View _view;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            if (_view == null || Element == null)
            {
                return;
            }
        }

        protected override void OnDetached()
        {
            _view = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "TranslationX" || args.PropertyName == "TranslationY")
            {
                MakeViewTranslation();
            }

            base.OnElementPropertyChanged(args);
        }

        public void MakeViewTranslation()
        {
            if (_view != null)
            {
                ObjectAnimator animatorX = ObjectAnimator.OfFloat(_view, nameof(_view.TranslationX), _view.TranslationX, _view.TranslationX);
                animatorX.SetDuration(1000);

                ObjectAnimator animatorY = ObjectAnimator.OfFloat(_view, nameof(_view.TranslationY), _view.TranslationY, _view.TranslationX);
                animatorY.SetDuration(1000);

                AnimatorSet animatorSet = new AnimatorSet();
                animatorSet.PlayTogether(animatorX, animatorY);
            }
        }
    }
}