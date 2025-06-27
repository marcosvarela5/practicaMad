using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecommendationService
    {
        /// <summary>
        /// Sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        IRecommendationDao RecommendationDao { set; }

        /// <summary>Recommends the event to group.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="justification">The justification.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        long RecommendEventToGroup(long eventId, long groupId, long userId, string justification);

        /// <summary>Finds the recommendations.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        RecBlock FindRecommendations(long userId, int startIndex, int count);

    }
}
