using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao{E, PK}" />
    public interface ICommentTableDao : IGenericDao<CommentTable, Int64>
    {

        /// <summary>Finds the by identifier.</summary>
        /// <param name="commId">The comm identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        CommentTable FindById(long commId);


        /// <summary>Finds the by event identifier and user identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<CommentTable> FindByEventIdAndUserId(long eventId, long userId, int startIndex, int count);


        /// <summary>Finds the by event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<CommentTable> FindByEventId(long eventId, int startIndex, int count);


        /// <summary>Finds the by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<CommentTable> FindByUserId(long userId, int startIndex, int count);


        /// <summary>Finds the by date range and user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<CommentTable> FindByDateRangeAndUserId(long userId, DateTime startDate, DateTime endDate, int startIndex, int count);


        /// <summary>Finds the by date range and event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<CommentTable> FindByDateRangeAndEventId(long eventId, DateTime startDate, DateTime endDate, int startIndex, int count);



    }
}