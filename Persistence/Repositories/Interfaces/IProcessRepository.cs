using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Query;

namespace Persistence.Repositories.Interfaces
{
    public interface IProcessRepository
    {
        IList<Process> GetAll();
        IList<Process> GetByQueryValues(ProcessQuery processQueryValues);
        Process Get(long? processId);
    }
}