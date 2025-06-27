using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao{E, PK}" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.UserProfile">
    /// </seealso>
    ///  />
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);
    }
}
