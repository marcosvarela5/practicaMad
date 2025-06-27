using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass()]
    public class IGroupServiceTest
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
        private const string groupName1 = "grupomad1";
        private const string groupName2 = "grupomad2";
        private const string groupName3 = "grupomad3";
        private static IKernel kernel;
        private static IGroupService groupService;
        private static IGroupDao groupDao;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IEventService eventService;
        private static ICategoryService categoryService;
        private static IEventDao eventDao;

        public IGroupServiceTest() { }

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
        }

        //Funcionalidad número 3. Gestion de grupos de usuario. Creación de grupos


        [TestMethod()]
        public void CreateGroupTest()
        {
            using (var scope = new TransactionScope())
            {

                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var groupId = groupService.CreateGroup(groupName1, "We can talk about football", categoryId, userId);

                GroupTable foundGroup = groupDao.Find(groupId);

                Assert.AreEqual(foundGroup.groupName, groupName1);
                Assert.AreEqual(foundGroup.body, "We can talk about football");
                Assert.AreEqual(foundGroup.categoryId, categoryId);

            }
        }

        //Funcionalidad número 3. Gestion de grupos de usuario. Unirse a un grupo

        [TestMethod()]
        public void JoinGroupTest()
        {
            using (var scope = new TransactionScope())
            {

                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var groupId = groupService.CreateGroup(groupName1, "We can talk about football", categoryId, userId);

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                groupService.JoinGroup(userId2, groupId);

                Assert.IsTrue(groupDao.IsUserMemberOfGroup(userId2, groupId));

            }

        }

        //Funcionalidad número 3. Gestion de grupos de usuario. Dejar un grupo

        [TestMethod()]
        public void LeaveGroupTest()
        {
            using (var scope = new TransactionScope())
            {

                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var groupId = groupService.CreateGroup(groupName1, "We can talk about football", categoryId, userId);

                //El usuario 2 se une al grupo

                groupService.JoinGroup(userId2, groupId);

                //Despues de unirse, ahora quiere dejar el grupo
                groupService.LeaveGroup(userId2, groupId);

                
                Assert.IsFalse(groupDao.IsUserMemberOfGroup(userId2, groupId));

            }

        }

        //Funcionalidad número 3. Gestion de grupos de usuario. Obtener todos los grupos

        [TestMethod()]
        public void GetAllGroupsTest()
        {
            using (var scope = new TransactionScope()) {

                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var categoryId2 = categoryService.AddCategory("Basketball");

                var categoryId3 = categoryService.AddCategory("Tennis");

                var groupId = groupService.CreateGroup(groupName1, "We can talk about football", categoryId, userId);

                var groupId2 = groupService.CreateGroup(groupName2, "We can talk about basketball", categoryId2, userId);

                var groupId3 = groupService.CreateGroup(groupName3, "We can talk about tennis", categoryId3, userId);

                List<GroupWithNumberOfMembers> groups = groupService.GetAllGroups(0,10);

                Assert.AreEqual(groups.Count, 3);

                Assert.AreEqual(groups[0].groupName, groupName1);
                Assert.AreEqual(groups[1].groupName, groupName2);
                Assert.AreEqual(groups[2].groupName, groupName3);

            }


        }

        //Funcionalidad número 3. Gestion de grupos de usuario. Obtener los grupos de un usuario

        [TestMethod()]
        public void GetUserGroupsTest()
        {
            using (var scope = new TransactionScope()) {
                var userId = userService.RegisterUser(loginName2, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var userId2 = userService.RegisterUser(loginName3, password, new UserProfileDetails(firstName, lastName, email, language, country));

                var categoryId = categoryService.AddCategory("Football");

                var categoryId2 = categoryService.AddCategory("Basketball");

                var categoryId3 = categoryService.AddCategory("Tennis");

                var groupId = groupService.CreateGroup(groupName1, "We can talk about football", categoryId, userId);

                var groupId2 = groupService.CreateGroup(groupName2, "We can talk about basketball", categoryId2, userId);

                var groupId3 = groupService.CreateGroup(groupName3, "We can talk about tennis", categoryId3, userId2);

                List<GroupWithNumberOfMembers> groups = groupService.GetUserGroups(userId,0,10);

                Assert.AreEqual(groups.Count, 2);

                Assert.AreEqual(groups[0].groupName, groupName1);
                Assert.AreEqual(groups[1].groupName, groupName2);

            }

        }
    }
}

