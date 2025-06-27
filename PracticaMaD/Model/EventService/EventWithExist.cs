using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
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
    public class EventWithExist
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public List<EventWithCatName> events { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EventWithExist"/> is exist.
        /// </summary>
        /// <value>
        ///   <c>true</c> if exist; otherwise, <c>false</c>.
        /// </value>
        public Boolean exist { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWithExist"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="exist">if set to <c>true</c> [exist].</param>
        public EventWithExist(List<EventWithCatName> events, Boolean exist)
        {
            this.events = events;
            this.exist = exist;
        }
    }
}
