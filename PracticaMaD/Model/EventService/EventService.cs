using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
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
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.EventService.IEventService" />
    public class EventService : IEventService

    {
        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>
        /// Sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Inject]
        public IEventDao EventDao { private get; set; }


        /// <summary>Finds the events.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        public EventWithExist FindEvents(string keywords, long? categoryId, int startIndex, int count)
        {
           
            var keywordList = (keywords == null)
                ? new List<string>()
                : keywords.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<EventWithCatName> events;
            if (categoryId.HasValue)
            {
                events = EventDao.FindEventsByCategory(null, categoryId.Value, startIndex, count);
            }
            else
            {
                events = EventDao.FindAllEvents(startIndex, count + 1);
            }

            var filteredEvents = events
                .Where(e => keywordList.All(kw => e.eventName.ToLower().Contains(kw)))
                .Take(count)
                .ToList();

            Boolean exist = events.Count > count;

            if (exist)
            {
                events.RemoveAt(count);
            }

            return new EventWithExist(events, exist);
        }

        /// <summary>Adds the event.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="envetDate">The envet date.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="reseña">The reseña.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long addEvent(long userId, string eventName, DateTime envetDate, long categoryId, string reseña)
        {
            if (!UserProfileDao.Exists(userId)) { 
                throw new InstanceNotFoundException(userId, typeof(UserProfile).FullName);
            }

            EventTable eventTable = new EventTable();

            eventTable.eventName = eventName;
            eventTable.eventDate = envetDate;
            eventTable.categoryId = categoryId;
            eventTable.reseña = reseña;
            eventTable.hasComments = false;

            EventDao.Create(eventTable);
            EventDao.Update(eventTable);

            return eventTable.eventId;
        }


        /// <summary>
        /// Finds the events by category.
        /// <param name="eventId">The eventId.</param>
        /// </summary>
        /// <returns></returns>
        public EventTable FindByEventId(long eventId)
        {
            return EventDao.FindByEventId(eventId);
        }
    }
}
