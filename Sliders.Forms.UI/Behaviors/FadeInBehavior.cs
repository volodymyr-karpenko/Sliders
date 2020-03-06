using System.ComponentModel;
using Xamarin.Forms;

namespace Sliders.Forms.UI.Behaviors
{
    public class FadeInBehavior : Behavior<View>
    {
        private double initialOpacity = default(double);

        protected override void OnAttachedTo(View bindable)
        {
            if (bindable != null)
            {
                initialOpacity = bindable.Opacity;
                bindable.Opacity = 0;
                bindable.PropertyChanged += OnIsVisibleChanged;
            }
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            if (bindable != null)
            {
                bindable.Opacity = initialOpacity;
                bindable.PropertyChanged -= OnIsVisibleChanged;
            }
            base.OnDetachingFrom(bindable);
        }

        private async void OnIsVisibleChanged(object sender, PropertyChangedEventArgs args)
        {
            View view = sender as View;
            if (view == null)
            {
                return;
            }

            if (args.PropertyName == "IsVisible")
            {
                if (view.IsVisible)
                {
                    await view.FadeTo(initialOpacity, 400, Easing.Linear);
                }
                else
                {
                    view.Opacity = 0;
                }
            }
        }
    }
}