using MvvmCross.Plugin.Messenger;

namespace Sliders.Core.Models
{
    public class ReadDataMessage : MvxMessage
    {
        public ReadDataMessage(object sender) : base(sender)
        {
        }
    }
}