namespace Commons.UnitOfWork.Context
{
    public class DefaultUnitOfWorkContext : IMutableUnitOfWorkContext
    {
        private IUnitOfWork? unitOfWork;
        public IUnitOfWork Current
        {
            get => unitOfWork ?? throw new InvalidOperationException("No current UnitOfWork is set.");
            set => unitOfWork = value;
        }
    }
}
