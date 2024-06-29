using System.Data;

namespace Commons.UnitOfWork.Middleware
{
    public class UnitOfWorkMiddlewareOptions
    {
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
    }
}
