using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.RecommendationService.IRecommendationService" />
    public class RecommendationService : IRecommendationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationService"/> class.
        /// </summary>
        public RecommendationService() { }

        /// <summary>
        /// Sets the group DAO.
        /// </summary>
        /// <value>
        /// The group DAO.
        /// </value>
        [Inject]
        public IGroupDao GroupDao { private get; set; }

        /// <summary>
        /// Sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Inject]
        public IEventDao EventDao { private get; set; }

        /// <summary>
        /// Sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        [Inject]
        public IRecommendationDao RecommendationDao { private get; set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>Finds the recommendations.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public RecBlock FindRecommendations(long userId, int startIndex, int count)
        {
            if (!UserProfileDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId, typeof(UserProfile).FullName);
            }

            List<Recommendation> recommendations = RecommendationDao.FindRecommendationsByUserId(userId, startIndex, count + 1);
            bool existsMore = recommendations.Count == count + 1;

            if (existsMore)
            {
                recommendations.RemoveAt(recommendations.Count - 1);
            }

            List<RecData> recDataList = recommendations.Select(r => new RecData(
                r.recomId,
                r.recommendationDate,
                r.groupId,
                r.GroupTable.groupName,
                r.justification,
                r.eventId,
                r.EventTable.eventName,
                r.EventTable.hasComments
            )).ToList();

            return new RecBlock(recDataList, existsMore);
        }



        /// <summary>Recommends the event to group.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="justification">The justification.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long RecommendEventToGroup(long eventId, long groupId, long userId, string justification)
        {
            if (!GroupDao.Exists(groupId))
            {
                throw new InstanceNotFoundException(groupId,
                    typeof(GroupTable).FullName);
            }

            if (!EventDao.Exists(eventId))
            {
                throw new InstanceNotFoundException(eventId,
                    typeof(Recommendation).FullName);
            }

            if(!UserProfileDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);
            }

            Recommendation recommendation = new Recommendation();

            recommendation.eventId = eventId;
            recommendation.groupId = groupId;
            recommendation.userId = userId;
            recommendation.recommendationDate = DateTime.Now;
            recommendation.justification = justification;

            RecommendationDao.Create(recommendation);
            RecommendationDao.Update(recommendation);

            return recommendation.recomId;
        }

        


    }
}
