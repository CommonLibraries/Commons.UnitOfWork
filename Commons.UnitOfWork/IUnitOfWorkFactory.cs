namespace Commons.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default);
    }
}
