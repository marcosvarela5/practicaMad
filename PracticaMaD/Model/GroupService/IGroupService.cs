using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Sets the group DAO.
        /// </summary>
        /// <value>
        /// The group DAO.
        /// </value>
        [Inject]
        IGroupDao GroupDao { set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>Creates the group.</summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="body">The body.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        long CreateGroup(string groupName,string body, long categoryId, long userId);

        /// <summary>Joins the group.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        [Transactional]
        void JoinGroup(long userId, long groupId);

        /// <summary>Leaves the group.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        [Transactional]
        void LeaveGroup(long userId, long groupId);

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<GroupWithNumberOfMembers> GetAllGroups(int startIndex, int count);

        /// <summary>
        /// Gets the user groups.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<GroupWithNumberOfMembers> GetUserGroups(long userId, int startIndex, int count);

        /// <summary>
        /// Determines whether [is user member of group] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user member of group] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        Boolean IsUserMemberOfGroup(long userId, long groupId);
    }
}
