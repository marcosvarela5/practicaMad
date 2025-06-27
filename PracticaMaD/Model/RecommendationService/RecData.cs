using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    [Serializable()]
    public class RecData
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long id { get; private set; }
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime date { get; private set; }

        /// <summary>
        /// Gets the group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public long groupId { get; private set; }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        public String groupName { get; private set; }

        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public long eventId { get; private set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public String eventName { get; private set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public String text { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has comments.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has comments; otherwise, <c>false</c>.
        /// </value>
        public Boolean hasComments { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecData"/> class.
        /// </summary>
        /// <param name="rId">The r identifier.</param>
        /// <param name="date">The date.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="ec">if set to <c>true</c> [ec].</param>
        public RecData(long rId, DateTime date,long groupId,String groupName, String desc, long eventId, String eventName, Boolean ec)
        {
            this.id = rId;
            this.date = date;
            this.groupId = groupId;
            this.groupName = groupName;
            this.eventId = eventId;
            this.eventName = eventName;
            this.text = desc;
            this.hasComments = ec;
        }
    }
}
