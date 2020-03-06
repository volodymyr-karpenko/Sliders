using MvvmCross.Plugin.Messenger;

namespace Sliders.Core.Models
{
    public class SlidersDataMessage : MvxMessage
    {
        public SlidersDataMessage(object sender, SlidersData data) : base(sender)
        {
            Data = data;
        }

        public SlidersData Data { get; set; }
    }
}