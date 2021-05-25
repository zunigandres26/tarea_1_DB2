using System.Collections.Generic;
using System.Threading.Tasks;

using Programa.Core.Models;

namespace Programa.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();

        Task<IEnumerable<SampleOrder>> GetGridDataAsync();

        Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();
    }
}
