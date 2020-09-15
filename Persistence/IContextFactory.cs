using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence
{
    public interface IContextFactory
    {
        EndoscopesTrackingContext CreateContext();
    }
}