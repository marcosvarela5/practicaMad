using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentTableService
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommentTableService
    {
        /// <summary>
        /// Sets the comment table DAO.
        /// </summary>
        /// <value>
        /// The comment table DAO.
        /// </value>
        ICommentTableDao CommentTableDao { set; }

        /// <summary>Adds the comment.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="body">The body.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        long AddComment(long eventId, long userId, string body);

        /// <summary>Updates the comment.</summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="body">The body.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        CommentTable UpdateComment(long commentId, long userId, string body);


        /// <summary>Deletes the comment.</summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        void DeleteComment(long commentId, long userId);

        /// <summary>Finds the event comments.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Transactional]
        List<CommentTable> FindEventComments(long eventId, int startIndex, int count);

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="commId">The comm identifier.</param>
        /// <returns></returns>
        [Transactional]
        CommentTable FindComment(long commId);
    }
}
