using BeastBattler.Backend.Database;
using BeastBattler.Backend.Database.DataModels;
using BeastBattler.Backend.Database.QueryModels;
using Microsoft.EntityFrameworkCore;

namespace BeastBattler.Backend.Repositories
{
    public class CreatureRepository : BaseRepository<Creature, CreatureQuery>
    {
        public CreatureRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Creature> ApplyRelations(IQueryable<Creature> query)
        {
            query = query
                .Include(x => x.User)
                .Include(x => x.Cage);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}
