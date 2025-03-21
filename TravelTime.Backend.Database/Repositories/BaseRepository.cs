using TravelTime.Backend.Database;
using TravelTime.Backend.Database.DataModels;
using TravelTime.Backend.Database.QueryModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TravelTime.Backend.Repositories
{
    public abstract class BaseRepository<T, Q>
           where T : BaseDataModel, new()
           where Q : BaseQueryModel
    {
        protected readonly DatabaseContext context;

        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
        }

        #region GetAsync
        /// <summary>
        /// This just provides the most basic functionality, should be overriden to get more fuctions 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(int entityId, bool includeRelated = false)
        {
            T result = null;

            if (!includeRelated)
                result = await context.Set<T>().FindAsync(entityId);
            else
            {
                var query = context.Set<T>().AsQueryable();
            
                // Relations
                query = this.ApplyRelations(query);

                result = await query.SingleOrDefaultAsync(x => x.Id == entityId);
            }

            return result;
        }
        #endregion

        #region GetAllAsync
        /// <summary>
        /// This just provides the most basic functionality, should be overriden to get more fuctions 
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        public virtual async Task<BaseQueryResult<T>> GetAllAsync(Q queryModel, bool includeRelated = false, bool disableTracking = true)
        {
            var result = new BaseQueryResult<T>();
            var query = context.Set<T>().AsQueryable();

            // Filtering
            query = ApplyFiltering(query, queryModel);

            // Count
            result.Count = await query.CountAsync();

            // Relations
            if (includeRelated)
                query = this.ApplyRelations(query);

            // Querying
            if (disableTracking)
                query = query.AsNoTracking();

            result.Entities = await query.ToArrayAsync();

            return result;
        }
        #endregion

        #region GetAllAsyncEnumerable
        /// <summary>
        /// This just provides the most basic functionality, should be overriden to get more fuctions 
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        public virtual IAsyncEnumerable<T> GetAllAsyncEnumerable(Q queryModel, bool includeRelated = false, bool disableTracking = true)
        {
            var query = this.context.Set<T>().AsQueryable();

            // Filtering
            query = ApplyFiltering(query, queryModel);

            // Relations
            if (includeRelated)
                query = this.ApplyRelations(query);

            // Querying
            if (disableTracking)
                query = query.AsNoTracking();

            return query.AsAsyncEnumerable();
        }
        #endregion

        #region ApplyFiltering
        protected virtual IQueryable<T> ApplyFiltering(IQueryable<T> query, Q queryModel)
        {
            return query;
        }
        #endregion

        #region ApplyOrderingMap
        protected virtual Dictionary<string, Expression<Func<T, object>>> ApplyOrderingMap()
        {
            return new Dictionary<string, Expression<Func<T, object>>>()
            {
                ["id"] = x => x.Id,
                ["createdAt"] = x => x.CreatedAt,
                //["deletedAt"] = x => x.DeletedAt,
            };
        }
        #endregion

        #region ApplyRelations
        protected virtual IQueryable<T> ApplyRelations(IQueryable<T> query)
        {
            return query;
        }
        #endregion

        #region Add
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        #endregion

        #region Remove
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        #endregion

        #region Attach
        public void Attach(T entity)
        {
            bool isDetached = context.Entry(entity).State == EntityState.Detached;
            if (isDetached)
                context.Set<T>().Attach(entity);
        }
        #endregion

        #region Detach
        public void Detach(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }
        #endregion
    }
}