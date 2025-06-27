using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class AddGroup : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblGroupCreated.Visible = false;
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            ICategoryService categoryService = iocManager.Resolve<ICategoryService>();

            var categories = categoryService.GetAllCategories();

            ddlCategory.DataSource = categories;
            ddlCategory.DataTextField = "categoryName";
            ddlCategory.DataValueField = "categoryId";
            ddlCategory.DataBind();
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IGroupService groupService = iocManager.Resolve<IGroupService>();

            try
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                string groupName = txtName.Text;
                string groupDescription = txtDescription.Text;
                long categoryId = long.Parse(ddlCategory.SelectedValue);

                long groupId = groupService.CreateGroup(groupName, groupDescription, categoryId, userId);
                lblGroupCreated.Visible = true;
                lblGroupCreated.Text = "Grupo creado con éxito.";
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Group/AllGroups.aspx"));
            }
            catch (InstanceNotFoundException)
            {
                lblGroupCreated.Visible = true;
                lblGroupCreated.Text = "Error: Usuario no encontrado.";
            }
            catch (Exception ex)
            {
                lblGroupCreated.Visible = true;
                lblGroupCreated.Text = "Error: " + ex.Message;
            }
        }
    }
}