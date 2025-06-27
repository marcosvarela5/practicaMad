using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework{E, PK}" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.EventDao.IEventDao" />
    public class EventDaoEntityFramework : GenericDaoEntityFramework<EventTable, Int64>, IEventDao
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDaoEntityFramework"/> class.
        /// </summary>
        public EventDaoEntityFramework() { }

        /// <summary>Finds all events.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EventWithCatName> FindAllEvents(int startIndex, int count)
        {
            DbSet<EventTable> events = Context.Set<EventTable>();
            List<EventTable> result = (from i in events
                                       orderby i.eventDate descending
                                       select i).Skip(startIndex).Take(count).ToList();

            List<EventWithCatName> result2 = new List<EventWithCatName>();

            foreach (EventTable e in result)
            {
                result2.Add(new EventWithCatName(e.eventId, e.eventName, e.reseña, e.eventDate, e.Category.categoryName, e.hasComments));
            }

            return result2;
        }

        /// <summary>
        /// Finds an event by eventId
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>The Event</returns>
        /// <exception cref="InstanceNotFoundException"/>

        public EventTable FindByEventId(long eventId)
        {
            EventTable result = null;

            #region Linq search.

            DbSet<EventTable> events = Context.Set<EventTable>();

            var result1 = (from i in events
                           where eventId == i.eventId
                           select i);

            result = result1.FirstOrDefault();

            #endregion Linq search.

            if (result == null)
                throw new InstanceNotFoundException(eventId,
                    typeof(EventTable).FullName);

            return result;

        }

        /// <summary>Finds the events by category.</summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EventWithCatName> FindEventsByCategory(string name, long categoryId, int startIndex, int count)
        {
            DbSet<EventTable> events = Context.Set<EventTable>();

            List<EventTable> result = (from i in events
                                       where categoryId == i.categoryId
                                       orderby i.eventDate descending
                                       select i).Skip(startIndex).Take(count).ToList();

            List<EventWithCatName> result2 = new List<EventWithCatName>();

            foreach (EventTable e in result)
            {
                result2.Add(new EventWithCatName(e.eventId, e.eventName, e.reseña , e.eventDate, e.Category.categoryName, e.hasComments));
            }

            return result2;
        }


    }
}
