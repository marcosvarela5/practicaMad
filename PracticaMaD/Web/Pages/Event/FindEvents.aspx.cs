using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Reflection;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
    public partial class FindEvents : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Category> cats = GetCategories();
                FillDropCategories(cats);
            }
        }

        private void FillDropCategories(List<Category> cats)
        {
            this.dropCategory.DataTextField = "categoryName";
            this.dropCategory.DataValueField = "categoryId";
            this.dropCategory.DataSource = cats;
            this.dropCategory.DataBind();
            this.dropCategory.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            this.dropCategory.SelectedIndex = 0;
        }

        private List<Category> GetCategories()
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            ICategoryService categoryService = iocManager.Resolve<ICategoryService>();

            return categoryService.GetAllCategories();
        }

        protected void BtnFindEventsClick(object sender, EventArgs e)
        {
            string keys = this.txtKeys.Text;
            string catId = this.dropCategory.SelectedValue;

            /* Do action. */
            String url = String.Format("./EventsFounded.aspx?words={0}&categoryId={1}", keys, catId);

            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        

        
    }
}