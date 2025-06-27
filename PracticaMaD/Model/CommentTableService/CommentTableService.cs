using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;

using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentTableService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.CommentTableService.ICommentTableService" />
    public class CommentTableService : ICommentTableService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentTableService"/> class.
        /// </summary>
        public CommentTableService() { }

        /// <summary>
        /// Sets the comment table DAO.
        /// </summary>
        /// <value>
        /// The comment table DAO.
        /// </value>
        [Inject]
        public ICommentTableDao CommentTableDao { private get; set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>
        /// Sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Inject]
        public IEventDao EventDao { private get; set; }

        /// <summary>
        ///   <br />
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <param name="body"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="System.Exception">El cuerpo del comentario no puede estar vacío</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        [Transactional]
        public long AddComment(long eventId, long userId, string body)
        {
            if (!EventDao.Exists(eventId))
            {
                throw new InstanceNotFoundException(eventId,
                    typeof(EventTable).FullName);
            }

            if (!UserProfileDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);
            }

            if (body == "")
            {
                throw new Exception("El cuerpo del comentario no puede estar vacío");
            }

            UserProfile user = UserProfileDao.Find(userId);

            CommentTable comment = new CommentTable();
            comment.eventId = eventId;
            comment.userId = userId;
            comment.userName = user.loginName;
            comment.body = body;
            comment.commDate = DateTime.Now;


            CommentTableDao.Create(comment);
            CommentTableDao.Update(comment);

            EventTable eventTable = EventDao.FindByEventId(eventId);

            if (eventTable.hasComments == false)
            {
                eventTable.hasComments = true;
                EventDao.Update(eventTable);
            }

            return comment.commId;
        }

        /// <summary>Finds the event comments.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public List<CommentTable> FindEventComments(long eventId, int startIndex, int count)
        {
            if (!EventDao.Exists(eventId))
            {
                throw new InstanceNotFoundException(eventId,
                    typeof(EventTable).FullName);
            }

            List<CommentTable> comments;

            comments = CommentTableDao.FindByEventId(eventId, startIndex, count);

            return comments;
        }

        /// <summary>
        ///   <br />
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="System.Exception">No eres el dueño del comentario</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void DeleteComment(long commentId, long userId)
        {
            if (!CommentTableDao.Exists(commentId))
            {
                throw new InstanceNotFoundException(commentId,
                    typeof(CommentTable).FullName);
            }

            CommentTable comment = CommentTableDao.Find(commentId);

            if (comment.userId != userId)
            {
                throw new Exception("No eres el dueño del comentario");
            }

            long eventId = comment.eventId;

            CommentTableDao.Remove(commentId);



            if (CommentTableDao.FindByEventId(eventId, 0, 10).Count == 0)
            {
                EventTable eventTable = EventDao.Find(eventId);
                eventTable.hasComments = false;
                EventDao.Update(eventTable);
            }
        }

        /// <summary>
        ///   <br />
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <param name="body"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        /// <exception cref="System.Exception">No eres el dueño del comentario</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public CommentTable UpdateComment(long commentId, long userId, string body)
        {
            if (!CommentTableDao.Exists(commentId))
            {
                throw new InstanceNotFoundException(commentId,
                    typeof(CommentTable).FullName);
            }

            CommentTable comment = CommentTableDao.Find(commentId);

            if (comment.userId != userId)
            {
                throw new Exception("No eres el dueño del comentario");
            }

            comment.body = body;
            CommentTableDao.Update(comment);

            return comment;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="commId">The comm identifier.</param>
        /// <returns></returns>
        public CommentTable FindComment(long commId)
        { 
        
            return CommentTableDao.Find(commId);
        }
    }
}