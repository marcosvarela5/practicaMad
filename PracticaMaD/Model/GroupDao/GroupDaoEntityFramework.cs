using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupDao
{
    public class GroupDaoEntityFramework : GenericDaoEntityFramework<GroupTable, long>, IGroupDao
    {
        /// <summary>Finds all groups.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<GroupWithNumberOfMembers> FindAllGroups(int startIndex, int count)
        {
            DbSet<GroupTable> groups = Context.Set<GroupTable>();
            var result = (from g in groups
                          orderby g.groupId
                          select g).Skip(startIndex).Take(count).ToList();

            List<GroupWithNumberOfMembers> resultWithNumberOfMembers = new List<GroupWithNumberOfMembers>();
            foreach (var group in result)
            {
                var numberOfMembers = group.Memberships.Count;
                var numRecommendations = group.Recommendations.Count;
                resultWithNumberOfMembers.Add(new GroupWithNumberOfMembers(group.groupId, group.groupName, numberOfMembers, numRecommendations));
            }

            return resultWithNumberOfMembers;
        }

        /// <summary>Determines whether [is user member of group] [the specified user identifier].</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user member of group] [the specified user identifier]; otherwise, <c>false</c>.</returns>
        public bool IsUserMemberOfGroup(long userId, long groupId)
        {
            DbSet<UserProfile> users = Context.Set<UserProfile>();
            var isMember = (from u in users
                            where u.usrId == userId && u.Memberships.Any(g => g.groupId == groupId)
                            select u).Any();
            return isMember;
        }

        /// <summary>
        /// Finds the groups by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<GroupWithNumberOfMembers> FindGroupsByUserId(long userId, int startIndex, int count)
        {
            DbSet<GroupTable> groups = Context.Set<GroupTable>();
            var result = (from g in groups
                          where (g.userId == userId && g.Memberships.Any (m => m.userId == userId)) || g.Memberships.Any(m => m.userId == userId)
                          orderby g.groupId
                          select g).Skip(startIndex).Take(count).ToList();

            List<GroupWithNumberOfMembers> resultWithNumberOfMembers = new List<GroupWithNumberOfMembers>();

            foreach (var group in result)
            {
                var numberOfMembers = group.Memberships.Count;
                var numRecommendations = group.Recommendations.Count;
                resultWithNumberOfMembers.Add(new GroupWithNumberOfMembers(group.groupId, group.groupName, numberOfMembers, numRecommendations));
            }
            return resultWithNumberOfMembers;
        }


    }
}
