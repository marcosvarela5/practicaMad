using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* CategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            /* CategoryService */
            kernel.Bind<ICategoryService>().
                To<CategoryService>();

            /* CommentTableDao */
            kernel.Bind<ICommentTableDao>().
                To<CommentTableDaoEntityFramework>();

            /* CommentTableService */

            kernel.Bind<ICommentTableService>().
                To<CommentTableService>();

            /* EventDao */

            kernel.Bind<IEventDao>().
                To<EventDaoEntityFramework>();

            /* EventService */

            kernel.Bind<IEventService>().
                To<EventService>();

            /* GroupDao */

            kernel.Bind<IGroupDao>().
                To<GroupDaoEntityFramework>();

            /* GroupService */

            kernel.Bind<IGroupService>().
                To<GroupService>();

            /* RecommendationDao */

            kernel.Bind<IRecommendationDao>().
                To<RecommendationDaoEntityFramework>();

            /* RecommendationService */

            kernel.Bind<IRecommendationService>().
                To<RecommendationService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["practicaMaDEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}