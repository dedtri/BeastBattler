namespace BeastBattler.Backend.Database
{
    public class UnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        #region CompletedAsync
        public async Task SaveChangesAsync()
        {
            await this.databaseContext.SaveChangesAsync();
        }
        #endregion
    }
}
