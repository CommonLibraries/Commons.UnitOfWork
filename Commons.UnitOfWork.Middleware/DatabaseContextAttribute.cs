namespace Commons.UnitOfWork.Middleware
{
    public class DatabaseContextAttribute : Attribute
    {
        public string DatabaseContextKey { get; }

        public DatabaseContextAttribute(string databaseContextKey)
        {
            this.DatabaseContextKey = databaseContextKey;
        }
    }
}
