using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sliders.Core.Services
{
    public interface IDataService<T>
    {
        Task<bool> CreateDataAsync(T item);
        Task<IEnumerable<T>> ReadAllDataAsync();
        Task<T> ReadDataAsync(string id);
        Task<bool> UpdateDataAsync(T item);
        Task<bool> DeleteDataAsync(string id);
        Task<bool> DeleteAllDataAsync();
    }
}