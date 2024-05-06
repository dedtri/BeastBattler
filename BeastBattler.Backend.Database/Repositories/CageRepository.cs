using BeastBattler.Backend.Database;
using BeastBattler.Backend.Database.DataModels;
using BeastBattler.Backend.Database.QueryModels;
using Microsoft.EntityFrameworkCore;

namespace BeastBattler.Backend.Repositories
{
    public class CageRepository : BaseRepository<Cage, CageQuery>
    {
        public CageRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Cage> ApplyRelations(IQueryable<Cage> query)
        {
            query = query
                .Include(x => x.Creatures);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}
