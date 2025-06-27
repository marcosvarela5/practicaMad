using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupService
{
    public class GroupWithNumberOfMembers
    {

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
        public string groupName { get; private set; }

        /// <summary>
        /// Gets the number of members.
        /// </summary>
        /// <value>
        /// The number of members.
        /// </value>
        public int numberOfMembers { get; private set; }

        /// <summary>
        /// Gets the number recommendations.
        /// </summary>
        /// <value>
        /// The number recommendations.
        /// </value>
        public int numRecommendations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupWithNumberOfMembers"/> class.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="numberOfMembers">The number of members.</param>
        /// <param name="numRecommendations">The number recommendations.</param>
        public GroupWithNumberOfMembers(long groupId, string groupName, int numberOfMembers, int numRecommendations)
        {
            this.groupId = groupId;
            this.groupName = groupName;
            this.numberOfMembers = numberOfMembers;
            this.numRecommendations = numRecommendations;
        }
    }
}
