using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject.Syntax;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{

    public interface IRecommendationDao : IGenericDao<Recommendation, Int64>
    {
        /// <summary>Finds the recommendations by group identifier.</summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<Recommendation> FindRecommendationsByGroupId(long groupId, int startindex, int count);

        /// <summary>Finds the recommendation by event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<Recommendation> FindRecommendationByEventId(long eventId, int startindex, int count);

        /// <summary>Finds the recommendations by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startindex">The startindex.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<Recommendation> FindRecommendationsByUserId(long userId, int startindex, int count);
    }
}
