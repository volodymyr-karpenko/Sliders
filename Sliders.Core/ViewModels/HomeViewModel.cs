using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Sliders.Core.Models;
using Sliders.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sliders.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IGenerateDataService _generateDataService;
        private readonly IDataService<SlidersData> _dataService;
        private readonly IMvxMessenger _messenger;
        private MvxSubscriptionToken _token;

        public HomeViewModel(IGenerateDataService generateDataService, IDataService<SlidersData> dataService, IMvxMessenger messenger)
        {
            _generateDataService = generateDataService;
            _dataService = dataService;
            _messenger = messenger;
            Task.Run(() => CountTotalDataItemsAsync());
        }

        public string AppDescription => Constants.AppConstants.appDescription;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _helpIconSrc = "\uf059";
        public string HelpIconSrc
        {
            get => _helpIconSrc;
            set => SetProperty(ref _helpIconSrc, value);
        }

        private bool _isStopSessionVisible = false;
        public bool IsStopSessionVisible
        {
            get => _isStopSessionVisible;
            set => SetProperty(ref _isStopSessionVisible, value);
        }

        private bool _isAppDescriptionVisible = false;
        public bool IsAppDescriptionVisible
        {
            get => _isAppDescriptionVisible;
            set => SetProperty(ref _isAppDescriptionVisible, value);
        }        

        private SlidersData _currentData = new SlidersData();
        public SlidersData CurrentData
        {
            get => _currentData;
            set => SetProperty(ref _currentData, value);
        }

        private bool _isTimestampVisible = false;
        public bool IsTimestampVisible
        {
            get => _isTimestampVisible;
            set => SetProperty(ref _isTimestampVisible, value);
        }

        private string _totalDataItems = "0";
        public string TotalDataItems
        {
            get => _totalDataItems;
            set => SetProperty(ref _totalDataItems, value);
        }

        private bool _isDeleteAllButtonVisible = false;
        public bool IsDeleteAllButtonVisible
        {
            get => _isDeleteAllButtonVisible;
            set => SetProperty(ref _isDeleteAllButtonVisible, value);
        }

        public IMvxCommand StartSessionCommand => new MvxCommand(StartSession, () => !IsBusy);
        public IMvxAsyncCommand StopSessionCommand => new MvxAsyncCommand(StopSessionAsync);
        public IMvxAsyncCommand DeleteAllDataCommand => new MvxAsyncCommand(DeleteAllDataAsync, () => !_generateDataService.IsRunning);
        public IMvxCommand QuestionCommand => new MvxCommand(ShowDialogue);

        private void StartSession()
        {
            IsBusy = true;
            IsStopSessionVisible = true;
            _generateDataService.Start();
            _token = _messenger.SubscribeOnMainThread((ReadDataMessage msg) => ReadDataAsync());

            ReadDataAsync();
        }

        private async void ReadDataAsync()
        {
            SlidersData item = null;

            try
            {
                item = await _dataService.ReadDataAsync("last");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (item == null || item.Id == CurrentData.Id)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    ReadDataAsync();
                });

                return;
            }

            CurrentData = item;
            _messenger.Publish(new SlidersDataMessage(this, item));

            if (!IsTimestampVisible)
            {
                IsBusy = IsTimestampVisible;
                IsDeleteAllButtonVisible = IsTimestampVisible;
                IsTimestampVisible = true;
            }
        }

        private async Task StopSessionAsync()
        {
            _generateDataService.Stop();
            IsBusy = true;
            IsStopSessionVisible = false;
            IsTimestampVisible = false;

            if (_token != null)
            {
                _token.Dispose();
            }

            _messenger.Publish(new SlidersDataMessage(this, new SlidersData
            {
                Id = "stop",
                Time = DateTime.UtcNow,
                Slider1 = 0,
                Slider2 = 0,
                Slider3 = 0,
                Slider4 = 0,
                Slider5 = 0
            }));

            await CountTotalDataItemsAsync();
        }

        private async Task CountTotalDataItemsAsync()
        {
            IEnumerable<SlidersData> data = null;

            try
            {
                data = await _dataService.ReadAllDataAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            List<SlidersData> items = data as List<SlidersData>;
            TotalDataItems = items != null ? items.Count.ToString() : "0";
            IsDeleteAllButtonVisible = TotalDataItems != "0" && !IsTimestampVisible;
            IsBusy = false;
        }

        private async Task DeleteAllDataAsync()
        {
            IsBusy = true;

            try
            {
                Task<bool> task = _dataService.DeleteAllDataAsync();
                await task.ContinueWith(t => CountTotalDataItemsAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ShowDialogue()
        {
            if (HelpIconSrc == "\uf059")
            {
                HelpIconSrc = "\uf00d";
                IsAppDescriptionVisible = true;
            }
            else
            {
                HelpIconSrc = "\uf059";
                IsAppDescriptionVisible = false;
            }
        }
    }
}