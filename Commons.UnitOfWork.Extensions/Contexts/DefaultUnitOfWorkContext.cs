using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork.Extensions.Contexts;

public class DefaultUnitOfWorkContext : IMutableUnitOfWorkContext
{
    private IUnitOfWork? unitOfWork;
    public IUnitOfWork? Current
    {
        get => unitOfWork;
        set => unitOfWork = value;
    }
}
