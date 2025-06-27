using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.GroupService.IGroupService" />
    public class GroupService : IGroupService
    {
        /// <summary>
        /// Sets the group DAO.
        /// </summary>
        /// <value>
        /// The group DAO.
        /// </value>
        [Inject]
        public IGroupDao GroupDao { private get; set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }


        /// <summary>Creates the group.</summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="body">The body.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long CreateGroup(string groupName, string body,  long categoryId, long userId)
        {
            var user = UserProfileDao.Find(userId);

            // Verificar si el usuario existe
            if (!UserProfileDao.Exists(userId))
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);

            // Crear y guardar el nuevo grupo
            var group = new GroupTable
            {
                groupName = groupName,
                body = body,
                categoryId = categoryId,
                userId = userId,
                Memberships = new List<Membership>()
            };

            var membership = new Membership
            {
                userId = userId,
                groupId = group.groupId,
                joinDate = DateTime.Now,
                UserProfile = user,
                GroupTable = group
            };

            group.Memberships.Add(membership);
            user.Memberships.Add(membership);

            GroupDao.Create(group);
            UserProfileDao.Update(user);

            return group.groupId;
        }

        /// <summary>Joins the group.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException">El usuario no está en el grupo.</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void JoinGroup(long userId, long groupId)
        {
            // Obtener el perfil de usuario y el grupo
            var user = UserProfileDao.Find(userId);
            var group = GroupDao.Find(groupId);

            var membership = user.Memberships.FirstOrDefault(m => m.groupId == groupId);
            if (membership != null)
                throw new InstanceNotFoundException($"El usuario no está en el grupo.",
                    typeof(Membership).FullName);

            membership = new Membership
            {
                userId = userId,
                groupId = groupId,
                joinDate = DateTime.Now
            };

            // Añadir el grupo a la lista de grupos del usuario y actualizar
            user.Memberships.Add(membership);
            group.Memberships.Add(membership);
            UserProfileDao.Update(user);
            GroupDao.Update(group);
            
        }

        /// <summary>Leaves the group.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException">El usuario no está en el grupo.</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void LeaveGroup(long userId, long groupId)
        {
            // Obtener el perfil de usuario y el grupo
            var user = UserProfileDao.Find(userId);
            var group = GroupDao.Find(groupId);

            var membership = user.Memberships.FirstOrDefault(m => m.groupId == groupId);
            if (membership == null)
                throw new InstanceNotFoundException($"El usuario no está en el grupo.",
                    typeof(Membership).FullName);

            // Remover el grupo de la lista de grupos del usuario y actualizar
            user.Memberships.Remove(membership);
            group.Memberships.Remove(membership);
            UserProfileDao.Update(user);
            GroupDao.Update(group);
        }


        /// <summary>Gets all groups.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<GroupWithNumberOfMembers> GetAllGroups(int startIndex, int count)
        {
            return GroupDao.FindAllGroups(startIndex, count);
        }

        /// <summary>
        /// Gets the user groups.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public List<GroupWithNumberOfMembers> GetUserGroups(long userId, int startIndex, int count)
        {
            // Verificar si el usuario existe
            if (!UserProfileDao.Exists(userId))
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);

            return GroupDao.FindGroupsByUserId(userId,startIndex,count);
        }

        /// <summary>
        /// Determines whether [is user member of group] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user member of group] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsUserMemberOfGroup(long userId, long groupId)
        {
            return GroupDao.IsUserMemberOfGroup(userId, groupId);
        }
    }
}
