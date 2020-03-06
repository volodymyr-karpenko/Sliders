using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Sliders.Core.ViewModels;
using System.Threading.Tasks;

namespace Sliders.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }

        public override Task Startup()
        {
            return base.Startup();
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}