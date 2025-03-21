using TravelTime.Backend.Database;
using TravelTime.Backend.Database.DataModels;
using TravelTime.Backend.Database.QueryModels;
using Microsoft.EntityFrameworkCore;

namespace TravelTime.Backend.Repositories
{
    public class TripRepository : BaseRepository<Trip, TripQuery>
    {
        public TripRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        //protected override IQueryable<Cage> ApplyRelations(IQueryable<Cage> query)
        //{
        //    query = query
        //        .Include(x => x.Creatures);

        //    return base.ApplyRelations(query);
        //}
        #endregion
    }
}
