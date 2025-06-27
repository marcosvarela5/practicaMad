using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupDao
{
    
    public interface IGroupDao : IGenericDao<GroupTable, long>
    {
        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<GroupWithNumberOfMembers> FindAllGroups(int startIndex, int count);


        /// <summary>
        /// Finds the groups by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<GroupWithNumberOfMembers> FindGroupsByUserId(long userId, int startIndex, int count);


        /// <summary>Determines whether [is user member of group] [the specified user identifier].</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user member of group] [the specified user identifier]; otherwise, <c>false</c>.</returns>
        bool IsUserMemberOfGroup(long userId, long groupId);
    }
}
