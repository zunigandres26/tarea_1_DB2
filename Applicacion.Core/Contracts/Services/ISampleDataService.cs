using System.Collections.Generic;
using System.Threading.Tasks;

using Applicacion.Core.Models;

namespace Applicacion.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();
    }
}
