using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{

    public interface IEventDao : IGenericDao<EventTable, Int64>
    {
        /// <summary>Finds all events.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<EventWithCatName> FindAllEvents(int startIndex, int count);

        /// <summary>Finds the by event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        EventTable FindByEventId(long eventId);

        /// <summary>Finds the events by category.</summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<EventWithCatName> FindEventsByCategory(string name, long categoryId, int startIndex, int count);
    }
}
