using MvvmCross.Base;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sliders.Tests.Mocks
{
    public class MockDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();

        public override bool IsOnMainThread => true;

        public virtual Task<bool> ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return Task.FromResult(true);
        }

        public virtual Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return Task.FromResult(true);
        }

        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception)
            {
                if (!maskExceptions)
                    throw;

                return false;
            }
        }

        public Task ExecuteOnMainThreadAsync(Action action, bool maskExceptions = true)
        {
            return Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    if (!maskExceptions)
                        throw;
                }
            });
        }

        public async Task ExecuteOnMainThreadAsync(Func<Task> action, bool maskExceptions = true)
        {
            try
            {
                await action();
            }
            catch (Exception)
            {
                if (!maskExceptions)
                    throw;
            }
        }
    }
}