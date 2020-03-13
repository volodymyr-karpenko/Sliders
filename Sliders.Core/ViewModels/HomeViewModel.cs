using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Sliders.Core.Constants;
using Sliders.Core.Models;
using Sliders.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IsBusy = true;
            Task<IEnumerable<SlidersData>> task = CountTotalDataItemsAsync();
            task.ContinueWith(t =>
            {
                List<SlidersData> items = t.Result as List<SlidersData>;
                TotalDataItems = items != null ? items.Count.ToString() : "0";
                IsDeleteAllButtonVisible = TotalDataItems != "0" && !IsTimestampVisible;
                IsBusy = false;
            });
        }

        public string AppDescription => AppConstants.appDescription;

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
        public IMvxCommand StopSessionCommand => new MvxCommand(StopSession, () => !IsBusy);
        public IMvxCommand DeleteAllDataCommand => new MvxCommand(DeleteAllData, () => !IsBusy);
        public IMvxCommand QuestionCommand => new MvxCommand(ShowDialogue);

        private async void StartSession()
        {
            IsBusy = true;
            IsStopSessionVisible = true;
            _generateDataService.Start();
            _token = _messenger.SubscribeOnMainThread(async (ReadDataMessage msg) => await ReadDataAsync());

            Task task = ReadDataAsync();
            await task.ContinueWith(t => 
            {
                IsBusy = false;
                IsDeleteAllButtonVisible = false;
                IsTimestampVisible = true;
            });            
        }

        private async Task ReadDataAsync()
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

            CurrentData = item;
            _messenger.Publish(new SlidersDataMessage(this, item));
        }

        private async void StopSession()
        {
            _generateDataService.Stop();
            IsBusy = true;
            IsStopSessionVisible = false;
            IsTimestampVisible = false;

            if (_token != null)
            {
                _token.Dispose();
            }

            Task<IEnumerable<SlidersData>> task = CountTotalDataItemsAsync();
            await task.ContinueWith(t => 
            {
                List<SlidersData> items = t.Result as List<SlidersData>;
                TotalDataItems = items != null ? items.Count.ToString() : "0";
                IsDeleteAllButtonVisible = TotalDataItems != "0" && !IsTimestampVisible;
                IsBusy = false;

                CurrentData = new SlidersData
                {
                    Id = AppConstants.defaultDataId,
                    Time = DateTime.UtcNow,
                    Slider1 = 0,
                    Slider2 = 0,
                    Slider3 = 0,
                    Slider4 = 0,
                    Slider5 = 0
                };

                _messenger.Publish(new SlidersDataMessage(this, CurrentData));
            });                        
        }

        private async Task<IEnumerable<SlidersData>> CountTotalDataItemsAsync()
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

            return data;            
        }

        private async void DeleteAllData()
        {
            IsBusy = true;

            try
            {
                await _dataService.DeleteAllDataAsync();
                Task<IEnumerable<SlidersData>> task = CountTotalDataItemsAsync();
                await task.ContinueWith(t => 
                {
                    List<SlidersData> items = t.Result as List<SlidersData>;
                    TotalDataItems = items != null ? items.Count.ToString() : "0";
                    IsDeleteAllButtonVisible = TotalDataItems != "0" && !IsTimestampVisible;
                    IsBusy = false;
                });
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