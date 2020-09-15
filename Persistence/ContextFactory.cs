using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence
{
    public class ContextFactory : IContextFactory
    {
        private readonly string _connectionString;

        public ContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EndoscopesTrackingContext CreateContext()
        {
            return new EndoscopesTrackingContext(this._connectionString);
        }
    }
}