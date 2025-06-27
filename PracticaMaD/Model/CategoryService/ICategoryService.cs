using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        ICategoryDao CategoryDao { set; }

        /// <summary>
        /// Adds a Category
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>The categoryId</returns>
        
        [Transactional]
        long AddCategory(string categoryName);

        /// <summary>
        /// Find a category by its id
        /// </summary>
        /// <param name="categoryId">The categoryId</param>
        /// <returns>The category</returns>
        
        [Transactional]
        Category FindCategory(long categoryId);

        /// <summary>
        /// Finds a list of categories
        /// </summary>
        /// <returns>The list of categories</returns>
        
        [Transactional]
        List<Category> GetAllCategories();

    }
}
