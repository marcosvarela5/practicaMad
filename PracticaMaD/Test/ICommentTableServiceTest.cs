using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass()]
    public class ICommentTableServiceTest
    {
        private const string loginName2 = "Alejandro";
        private const string loginName3 = "Marcos";
        private const string loginName4 = "Santiago";
        private const string password = "password";
        private const string firstName = "firstName";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const string eventName1 = "Football Championship";
        private const string eventName2 = "Basketball Tournament";
        private const string eventName3 = "Football World Cup";

        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IEventService eventService;
        private static ICategoryService categoryService;
        private static IEventDao eventDao;
        private static ICommentTableService commentTableService;
        private static ICommentTableDao commentTableDao;

        public ICommentTableServiceTest() { }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            eventService = kernel.Get<IEventService>();
            categoryService = kernel.Get<ICategoryService>();
            eventDao = kernel.Get<IEventDao>();
            commentTableService = kernel.Get<ICommentTableService>();
            commentTableDao = kernel.Get<ICommentTableDao>();
        }

        //Funcionalidad número 5. Añadir un comentario a un evento

        [TestMethod()]
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId3 = userService.RegisterUser(loginName4, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                EventTable eventTable = eventDao.FindByEventId(eventId);

                Assert.IsFalse(eventTable.hasComments);

                var comment1 = commentTableService.AddComment(eventId, userId, "Comentario de Alejandro");
                var comment2 = commentTableService.AddComment(eventId, userId2, "Comentario de Marcos");
                var comment3 = commentTableService.AddComment(eventId, userId3, "Comentario de Santiago");

                List<CommentTable> comments = commentTableDao.FindByEventId(eventId, 0, 3);

                Assert.AreEqual(3, comments.Count);

                CommentTable foundComment1 = comments[2];
                Assert.AreEqual("Comentario de Alejandro", foundComment1.body);
                Assert.AreEqual(userId, foundComment1.userId);
                Assert.AreEqual(loginName2, foundComment1.userName);
                //Comprobamos que el evento tiene comentarios
                Assert.IsTrue(eventTable.hasComments);

                CommentTable foundComment2 = comments[1];
                Assert.AreEqual("Comentario de Marcos", foundComment2.body);
                Assert.AreEqual(userId2, foundComment2.userId);
                Assert.AreEqual(loginName3, foundComment2.userName);

                CommentTable foundComment3 = comments[0];
                Assert.AreEqual("Comentario de Santiago", foundComment3.body);
                Assert.AreEqual(userId3, foundComment3.userId);
                Assert.AreEqual(loginName4, foundComment3.userName);

            }

        }

        //Funcionalidad número 5. Añadir un comentario a un evento y borrarlo

        [TestMethod()]
        public void DeleteCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var comment1 = commentTableService.AddComment(eventId, userId, "Comentario de Alejandro");

                var eventTable = eventDao.FindByEventId(eventId);

                commentTableService.DeleteComment(comment1, userId);

                Assert.IsFalse(eventTable.hasComments);

            }

        }

        //Funcionalidad número 5. Añadir un comentario a un evento y modificarlo

        [TestMethod()]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var comment1 = commentTableService.AddComment(eventId, userId, "Comentario de Alejandro");

                var foundComment = commentTableDao.Find(comment1);

                Assert.AreEqual("Comentario de Alejandro", foundComment.body);

                commentTableService.UpdateComment(comment1, userId, "Comentario de Alejandro actualizado");

                var foundComment2 = commentTableDao.Find(comment1);

                Assert.AreEqual("Comentario de Alejandro actualizado", foundComment2.body);

            }

        }

        //Funcionalidad número 6. Ver comentarios de un evento

        [TestMethod()]
        public void FindEventCommentsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId3 = userService.RegisterUser(loginName4, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var comment1 = commentTableService.AddComment(eventId, userId, "Comentario de Alejandro");

                var comment2 = commentTableService.AddComment(eventId, userId2, "Comentario de Marcos");

                var comment3 = commentTableService.AddComment(eventId, userId3, "Comentario de Santiago");

                List<CommentTable> comments = commentTableService.FindEventComments(eventId, 0, 3);

                Assert.AreEqual(3, comments.Count);

                CommentTable foundComment1 = comments[2];
                Assert.AreEqual("Comentario de Alejandro", foundComment1.body);
                Assert.AreEqual(userId, foundComment1.userId);
                Assert.AreEqual(loginName2, foundComment1.userName);

                CommentTable foundComment2 = comments[1];
                Assert.AreEqual("Comentario de Marcos", foundComment2.body);
                Assert.AreEqual(userId2, foundComment2.userId);
                Assert.AreEqual(loginName3, foundComment2.userName);

                CommentTable foundComment3 = comments[0];
                Assert.AreEqual("Comentario de Santiago", foundComment3.body);
                Assert.AreEqual(userId3, foundComment3.userId);
                Assert.AreEqual(loginName4, foundComment3.userName);
            }
        }
    }
}