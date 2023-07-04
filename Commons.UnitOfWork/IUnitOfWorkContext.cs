namespace Commons.UnitOfWork
{
    public interface IUnitOfWorkContext
    {
        IUnitOfWork Create();
        Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default);
    }
}
