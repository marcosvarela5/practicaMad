using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    /// <summary>
    /// 
    /// </summary>
    public class EventWithCatName
    {
        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public long eventId { get; set; }
        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public string eventName { get; set; }
        /// <summary>
        /// Gets or sets the event description.
        /// </summary>
        /// <value>
        /// The event description.
        /// </value>
        public string eventDescription { get; set; }
        /// <summary>
        /// Gets or sets the event date.
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        public DateTime eventDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string categoryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has comments.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has comments; otherwise, <c>false</c>.
        /// </value>
        public Boolean hasComments { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="EventWithCatName"/> class.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="eventDescription">The event description.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="hasComments">if set to <c>true</c> [has comments].</param>
        public EventWithCatName(long eventId, string eventName, string eventDescription, DateTime eventDate, string categoryName, bool hasComments)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.eventDescription = eventDescription;
            this.eventDate = eventDate;
            this.categoryName = categoryName;
            this.hasComments = hasComments;
        }
    }
}
