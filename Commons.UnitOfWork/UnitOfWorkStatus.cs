namespace Commons.UnitOfWork;

public enum UnitOfWorkStatus
{
    NotStarted,
    Active,
    Committed,
    RolledBack
}
