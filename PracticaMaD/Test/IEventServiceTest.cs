using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IEventServiceTest
    {
        private const string loginName = "loginNameTest";
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

        

        public IEventServiceTest() { }

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
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }


        [TestMethod]
        public void addEventTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, new UserProfileDetails(firstName, lastName, email, language, country));
                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var foundEvent = eventDao.FindByEventId(eventId);

                Assert.AreEqual(eventName1, foundEvent.eventName);
                Assert.AreEqual(categoryId, foundEvent.categoryId);
                Assert.AreEqual("Reseña", foundEvent.reseña);
            }

        }

        //Funcionalidad número 4. Búsqueda eventos

        [TestMethod()]
        public void FindEventsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var eventId = eventService.addEvent(userId, eventName1, DateTime.Now, categoryId, "Reseña");

                var events = eventService.FindEvents("Football", categoryId, 0, 10);

                Assert.AreEqual(1, events.events.Count);

                Assert.AreEqual(eventName1, events.events[0].eventName);
            }

        }
    }
}
