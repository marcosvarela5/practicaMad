using System;
using System.Collections.Generic;
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
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao{E, PK}" />
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        /// <summary>
        /// Find a category by his name
        /// </summary>
        /// <param categoryName="name">The category name</param>
        /// <returns></returns>
        /// 
        Category FindByCategoryName(string categoryName);
    }
}
