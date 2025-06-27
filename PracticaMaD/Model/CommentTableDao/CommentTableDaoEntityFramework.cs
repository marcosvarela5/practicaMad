using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework{E, PK}" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao.ICommentTableDao" />
    public class CommentTableDaoEntityFramework : GenericDaoEntityFramework<CommentTable, Int64>, ICommentTableDao
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentTableDaoEntityFramework"/> class.
        /// </summary>
        public CommentTableDaoEntityFramework()
        {
        }

        /// <summary>Finds the by identifier.</summary>
        /// <param name="commId">The comm identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public CommentTable FindById(long commId) { return base.Find(commId); }

        /// <summary>Finds the by event identifier and user identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<CommentTable> FindByEventIdAndUserId(long eventId, long userId, int startIndex, int count)
        {

            DbSet<CommentTable> comments = Context.Set<CommentTable>();
            var result =
                    (from c in comments
                     where c.eventId == eventId && c.userId == userId
                     orderby c.commDate descending
                     select c).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <summary>Finds the by event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<CommentTable> FindByEventId(long eventId, int startIndex, int count)
        {
            DbSet<CommentTable> comments = Context.Set<CommentTable>();
            var result =
                    (from c in comments
                     where c.eventId == eventId
                     orderby c.commDate descending
                     select c).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <summary>Finds the by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<CommentTable> FindByUserId(long userId, int startIndex, int count)
        {
            DbSet<CommentTable> comments = Context.Set<CommentTable>();
            var result =
                    (from c in comments
                     where c.userId == userId
                     orderby c.commDate descending
                     select c).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <summary>Finds the by date range and user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<CommentTable> FindByDateRangeAndUserId(long userId, DateTime startDate, DateTime endDate, int startIndex, int count)
        {
            DbSet<CommentTable> comments = Context.Set<CommentTable>();
            var result =
                    (from c in comments
                     where c.userId == userId && c.commDate >= startDate && c.commDate <= endDate
                     orderby c.commDate descending
                     select c).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <summary>Finds the by date range and event identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<CommentTable> FindByDateRangeAndEventId(long eventId, DateTime startDate, DateTime endDate, int startIndex, int count)
        {
            DbSet<CommentTable> comments = Context.Set<CommentTable>();
            var result =
                    (from c in comments
                     where c.eventId == eventId && c.commDate >= startDate && c.commDate <= endDate
                     orderby c.commDate descending
                     select c).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}