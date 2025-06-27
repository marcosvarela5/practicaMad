using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework{E, PK}" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao.IRecommendationDao" />
    public class RecommendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, Int64>, IRecommendationDao
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationDaoEntityFramework"/> class.
        /// </summary>
        public RecommendationDaoEntityFramework() { }

        /// <summary>Finds the recommendations by group identifier.</summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Recommendation> FindRecommendationsByGroupId(long groupId, int startindex, int count)
        {
            DbSet<Recommendation> recommendations = Context.Set<Recommendation>();
            var result = (from r in recommendations
                          where r.groupId == groupId
                          select r).Skip(startindex).Take(count).ToList();
            return result;
        }

        /// <summary>Finds the recommendation by event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Recommendation> FindRecommendationByEventId(long eventId, int startindex, int count)
        {
            DbSet<Recommendation> recommendations = Context.Set<Recommendation>();
            var result = (from r in recommendations
                          where r.eventId == eventId
                          orderby r.recommendationDate ascending
                          select r).Skip(startindex).Take(count).ToList();
            return result;
        }

        /// <summary>Finds the recommendations by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Recommendation> FindRecommendationsByUserId(long userId, int startindex, int count)
        {
            DbSet<Recommendation> recommendations = Context.Set<Recommendation>();
            var result = (from r in recommendations
                          join g in Context.Set<GroupTable>() on r.groupId equals g.groupId
                          join m in Context.Set<Membership>() on g.groupId equals m.groupId
                          where m.userId == userId && r.userId != userId
                          orderby g.groupName, r.recommendationDate descending
                          select r).Skip(startindex).Take(count).ToList();
            return result;
        }

    }
}
