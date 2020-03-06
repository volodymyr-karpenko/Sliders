using Moq;
using MvvmCross.Plugin.Messenger;
using MvvmCross.Tests;
using Sliders.Core.Models;
using Sliders.Core.Services;
using Sliders.Core.ViewModels;
using System.Reflection;
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
        public void IsBusyFalseAfterStartSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void IsStopSessionVisibleTrueAfterStartSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            Assert.True(vm.IsStopSessionVisible);
        }

        [Fact]
        public void IsTimestampVisibleTrueAfterReadDataAsync()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            Assert.True(vm.IsTimestampVisible);
        }

        [Fact]
        public void GeneratorNotNullAfterStartSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StartSessionCommand.Execute();
            FieldInfo _generateDataService = vm.GetType().GetField("_generateDataService", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(_generateDataService.GetValue(vm));
        }

        [Fact]
        public void IsStopSessionVisibleFalseAfterStopSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StopSessionCommand.Execute();
            Assert.False(vm.IsStopSessionVisible);
        }

        [Fact]
        public void IsTimestampVisibleFalseAfterStopSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StopSessionCommand.Execute();
            Assert.False(vm.IsTimestampVisible);
        }

        [Fact]
        public void GeneratorNotNullAfterStopSessionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.StopSessionCommand.Execute();
            FieldInfo _generateDataService = vm.GetType().GetField("_generateDataService", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(_generateDataService.GetValue(vm));
        }

        [Fact]
        public void QuestionMarkIconTappedQuestionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.HelpIconSrc = "\uf059";
            vm.QuestionCommand.Execute();
            Assert.Equal("\uf00d", vm.HelpIconSrc);
            Assert.True(vm.IsAppDescriptionVisible);
        }

        [Fact]
        public void CloseIconTappedQuestionCommand()
        {
            HomeViewModel vm = Ioc.IoCConstruct<HomeViewModel>();
            vm.HelpIconSrc = "\uf00d";
            vm.QuestionCommand.Execute();
            Assert.Equal("\uf059", vm.HelpIconSrc);
            Assert.False(vm.IsAppDescriptionVisible);
        }
    }
}