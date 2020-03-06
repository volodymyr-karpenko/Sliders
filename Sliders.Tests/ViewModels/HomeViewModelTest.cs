using Moq;
using MvvmCross.Plugin.Messenger;
using MvvmCross.Tests;
using Sliders.Core.Models;
using Sliders.Core.Services;
using Sliders.Core.ViewModels;
using Xunit;

namespace Sliders.Tests.ViewModels
{
    public class HomeViewModelTest : MvxIoCSupportingTest
    {
        public HomeViewModelTest()
        {
            ClearAll();
            AdditionalSetup();
        }

        protected override void AdditionalSetup()
        {
            var generateDataService = new Mock<IGenerateDataService>();
            var dataService = new Mock<IDataService<SlidersData>>();
            var messenger = new Mock<IMvxMessenger>();
            Ioc.RegisterSingleton(generateDataService.Object);
            Ioc.RegisterSingleton(dataService.Object);
            Ioc.RegisterSingleton(messenger.Object);
        }

        [Fact]
        public void IsBusyEqualsTrueOnStartSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            Assert.True(vm.IsBusy);
        }

        [Fact]
        public void IsStopSessionVisibleEqualsTrueOnStartSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            Assert.True(vm.IsStopSessionVisible);
        }
    }
}