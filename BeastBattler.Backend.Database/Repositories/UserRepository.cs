using BeastBattler.Backend.Database;
using BeastBattler.Backend.Database.DataModels;
using BeastBattler.Backend.Database.QueryModels;
using Microsoft.EntityFrameworkCore;

namespace BeastBattler.Backend.Repositories
{
    public class UserRepository : BaseRepository<User, UserQuery>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        #region ConfirmLoginAsync
        public virtual User ConfirmLogin(User login)
        {
            return context.Users.SingleOrDefault(u => (u.Email == login.Email) && (u.Password == login.Password));
        }
        #endregion

        #region GetByNameAsync
        public virtual async Task<User> GetByEmailAsync(string email, bool includeRelated = false)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
        #endregion

        #region ApplyRelations
        protected override IQueryable<User> ApplyRelations(IQueryable<User> query)
        {
            query = query
                .Include(x => x.Creatures);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}
