namespace Sliders.Core.Services
{
    public interface IGenerateDataService
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}