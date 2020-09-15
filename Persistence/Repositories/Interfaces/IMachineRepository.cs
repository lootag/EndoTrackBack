using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IMachineRepository
    {
        IList<Machine> GetAll();

    }
}