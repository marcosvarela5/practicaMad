using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework{E, PK}"/>
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.CategoryDao.ICategoryDao" />
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        /// <summary>
        /// Public Constructor
        /// </summary>
        /// 
        public CategoryDaoEntityFramework()
        {
        }
        /// <summary>
        /// Finds a category by his name
        /// </summary>
        /// <param categoryName="name">The category name</param>
        /// <returns>The category</returns>
        /// 
        public Category FindByCategoryName(string categoryName)
        {
            Category category = null;
            DbSet<Category> categories = Context.Set<Category>();

            var result = (from c in categories
                          where c.categoryName.Equals(categoryName)
                          select c);
            category = result.FirstOrDefault();

            if (category == null)
                throw new InstanceNotFoundException(categoryName,
                    typeof(EventTable).FullName);

            return category;
        }

    }
}