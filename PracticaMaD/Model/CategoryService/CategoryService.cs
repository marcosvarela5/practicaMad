using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
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
    /// <seealso cref="Es.Udc.DotNet.PracticaMaD.Model.CategoryService.ICategoryService" />
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService() { }

        /// <summary>
        /// Sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        [Inject]
        public ICategoryDao categoryDao { private get; set; }

        /// <summary>
        /// Sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICategoryDao CategoryDao { set => throw new NotImplementedException(); }



        /// <summary>Adds a Category</summary>
        /// <param name="categoryName">The category name</param>
        /// <returns>The categoryId</returns>
        /// <exception cref="System.Exception">El nombre de la categoría no puede estar vacío</exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        [Transactional]
        public long AddCategory(string categoryName)
        {
            if (categoryName == "")
            {
                throw new Exception("El nombre de la categoría no puede estar vacío");
            }

            Category category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);
            categoryDao.Update(category);

            return category.categoryId;
        }

        /// <summary>Find a category by its id</summary>
        /// <param name="categoryId">The categoryId</param>
        /// <returns>The category</returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException">FindCategory</exception>
        [Transactional]
        public Category FindCategory(long categoryId)
        {
            if (categoryDao.Find(categoryId) != null)
            {
                return categoryDao.Find(categoryId);
            }
            else
            {
                throw new InstanceNotFoundException(categoryId, "FindCategory");
            }
        }



        /// <summary>Finds a list of categories</summary>
        /// <returns>The list of categories</returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException">Found 0 Categories</exception>
        [Transactional]
        public List<Category> GetAllCategories()
        {
            List<Category> allCategories = new List<Category>();
            if ((allCategories = categoryDao.GetAllElements()) != null)
            {
                return allCategories;
            }
            else
            {
                throw new InstanceNotFoundException(allCategories, "Found 0 Categories");
            }
        }

    }
}
