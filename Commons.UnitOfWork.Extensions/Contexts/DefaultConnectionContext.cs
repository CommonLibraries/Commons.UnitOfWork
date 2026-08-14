using System.Data;
using System.Data.Common;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork.Extensions.Contexts;

public class DefaultConnectionContext : IConnectionContext
{
    private readonly IUnitOfWorkContext unitOfWorkContext;
    public DefaultConnectionContext(IUnitOfWorkContext unitOfWorkContext)
    {
        this.unitOfWorkContext = unitOfWorkContext;
    }

    public IDbConnection Current
    {
        get => this.unitOfWorkContext.Current?.Connection ?? throw new InvalidOperationException("No connection is set in the current context.");
    }
}
