using Sliders.Core.Models;
using System;
using System.Diagnostics;
using System.Threading;

namespace Sliders.Core.Services
{
    public class GenerateDataService : IGenerateDataService
    {
        private readonly IDataService<SlidersData> _dataService;
        private readonly Timer _timer;

        public GenerateDataService(IDataService<SlidersData> dataService)
        {
            _dataService = dataService;
            _timer = new Timer(GenerateData);
        }

        public bool IsRunning { get; private set; }

        private async void GenerateData(object state)
        {
            var rng = new Random();

            SlidersData data = new SlidersData
            {
                Id = Guid.NewGuid().ToString(),
                Time = DateTime.UtcNow,
                Slider1 = rng.Next(-200, 200),
                Slider2 = rng.Next(-200, 200),
                Slider3 = rng.Next(-200, 200),
                Slider4 = rng.Next(-200, 200),
                Slider5 = rng.Next(-200, 200)
            };

            try
            {
                await _dataService.CreateDataAsync(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Start()
        {
            _timer.Change(0, 1000);
            IsRunning = true;
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            IsRunning = false;
        }
    }
}