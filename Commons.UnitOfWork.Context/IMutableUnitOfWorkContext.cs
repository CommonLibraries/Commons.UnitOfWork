namespace Commons.UnitOfWork.Context
{
    public interface IMutableUnitOfWorkContext : IUnitOfWorkContext
    {
        new IUnitOfWork Current { get; set; }
    }
}
