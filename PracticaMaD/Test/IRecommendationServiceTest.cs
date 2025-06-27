using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IRecommendationServiceTest
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
        private const string eventName4 = "Football Championship 2";
        private const string groupName1 = "grupomad1";
        private const string groupName2 = "grupomad2";
        private const string groupName3 = "grupomad3";

        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IEventService eventService;
        private static ICategoryService categoryService;
        private static IEventDao eventDao;
        private static ICommentTableService commentTableService;
        private static ICommentTableDao commentTableDao;
        private static IGroupDao groupDao;
        private static IGroupService groupService;
        private static IRecommendationService recommendationService;
        private static IRecommendationDao recommendationDao;


        public IRecommendationServiceTest() { }

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
            groupService = kernel.Get<IGroupService>();
            groupDao = kernel.Get<IGroupDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            userService = kernel.Get<IUserService>();
            eventService = kernel.Get<IEventService>();
            categoryService = kernel.Get<ICategoryService>();
            eventDao = kernel.Get<IEventDao>();
            commentTableService = kernel.Get<ICommentTableService>();
            commentTableDao = kernel.Get<ICommentTableDao>();
            recommendationService = kernel.Get<IRecommendationService>();
            recommendationDao = kernel.Get<IRecommendationDao>();
        }

        //Funcionalidad numero 7. Recomendar evento a un grupo

        [TestMethod()]
        public void RecommendEventToGroupTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var groupId = groupService.CreateGroup(groupName1, "Body", categoryId, userId);

                //El usuario 2 recomienda el evento al grupo

                var recomId = recommendationService.RecommendEventToGroup(eventId, groupId, userId2, "The event is about football");

                Recommendation recommendation = recommendationDao.Find(recomId);

                Assert.AreEqual(recommendation.eventId, eventId);
                Assert.AreEqual(recommendation.groupId, groupId);
                Assert.AreEqual(recommendation.userId, userId2);
                Assert.AreEqual(recommendation.justification, "The event is about football");
            }
        }

        //Funcionalidad numero 8. Mostrar recomendaciones

        [TestMethod()]
        public void FindRecommendationsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId3 = userService.RegisterUser(loginName4, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var categoryId2 = categoryService.AddCategory("Basketball");

                var categoryId3 = categoryService.AddCategory("Tennis");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var eventId2 = eventService.addEvent(userId, eventName2, DateTime.Now, categoryId2, "Reseña");

                var eventId3 = eventService.addEvent(userId, eventName3, DateTime.Now, categoryId3, "Reseña");

                var eventId4 = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var groupId = groupService.CreateGroup(groupName1, "Body", categoryId, userId);

                var groupId2 = groupService.CreateGroup(groupName2, "Body", categoryId2, userId);

                //User2 se une al grupo y recomienda el evento 1 "Football Championship"
                groupService.JoinGroup(userId2, groupId);

                var recomId = recommendationService.RecommendEventToGroup(eventId, groupId, userId2, "The event is about football");

                //User2 recomineda tambien el evento 4 Football Championship 2 al grupo 
                var recomId2 = recommendationService.RecommendEventToGroup(eventId4, groupId, userId2, "The event is about football");

                //User3 se une al grupo y recomienda el evento 2 "Basketball Tournament"
                groupService.JoinGroup(userId3, groupId2);
                var recomId3 = recommendationService.RecommendEventToGroup(eventId2, groupId2, userId3, "The event is about basketball");

                //Comprobamos que el usuario1 tiene 3 recomendaciones, 2 de ellas del usuario2 en el grupo 1
                //y una del usuario3 en el grupo 2

                var recommendations = recommendationService.FindRecommendations(userId, 0, 3);

                Assert.AreEqual(recommendations.recommendations.Count, 3);
                Assert.AreEqual(recommendations.recommendations[0].eventId, eventId);
                Assert.AreEqual(recommendations.recommendations[0].groupId, groupId);
                

                Assert.AreEqual(recommendations.recommendations[1].eventId, eventId4);
                Assert.AreEqual(recommendations.recommendations[1].groupId, groupId);
                

                Assert.AreEqual(recommendations.recommendations[2].eventId, eventId2);
                Assert.AreEqual(recommendations.recommendations[2].groupId, groupId2);
                
            }
        }
    }
}

