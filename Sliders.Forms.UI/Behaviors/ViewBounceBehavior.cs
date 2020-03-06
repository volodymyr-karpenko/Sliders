using System.ComponentModel;
using Xamarin.Forms;

namespace Sliders.Forms.UI.Behaviors
{
    public class ViewBounceBehavior : Behavior<View>
    {
        protected override void OnAttachedTo(View bindable)
        {
            if (bindable != null)
            {
                bindable.PropertyChanged += OnIsVisibleOrTextChanged;
            }
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            if (bindable != null)
            {
                bindable.PropertyChanged -= OnIsVisibleOrTextChanged;
            }
            base.OnDetachingFrom(bindable);
        }

        private async void OnIsVisibleOrTextChanged(object sender, PropertyChangedEventArgs args)
        {
            View view = sender as View;
            if (view == null)
            {
                return;
            }
            if ((args.PropertyName == "IsVisible" && view.IsVisible) || (args.PropertyName == "Text" && view.IsVisible))
            {
                await view.ScaleTo(1.2, 100, Easing.Linear);
                await view.ScaleTo(1, 500, Easing.BounceOut);
            }
        }
    }
}