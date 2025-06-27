using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Inject]
        IEventDao EventDao { set; }

        /// <summary>Adds the event.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="envetDate">The envet date.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="reseña">The reseña.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        long addEvent(long userId, string eventName, DateTime envetDate, long categoryId, string reseña);

        /// <summary>Finds the events.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        EventWithExist FindEvents(string keywords, long? categoryId, int startIndex, int count);

        /// <summary>Finds the events by category.
        /// <param name="eventId">The eventId.</param>
        /// </summary>S
        [Transactional]
        EventTable FindByEventId(long eventId);
    }
}
