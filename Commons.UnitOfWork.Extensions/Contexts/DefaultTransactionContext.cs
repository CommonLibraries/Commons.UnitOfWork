using System.Data;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork.Extensions.Contexts;

public class DefaultTransactionContext : ITransactionContext
{
    private readonly IUnitOfWorkContext unitOfWorkContext;
    public DefaultTransactionContext(IUnitOfWorkContext unitOfWorkContext)
    {
        this.unitOfWorkContext = unitOfWorkContext;
    }

    public IDbTransaction? Current
    {
        get => this.unitOfWorkContext.Current?.Transaction;
    }
}
