using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class AllGroups : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int startIndex, count;
                lnkPrevious.Visible = false;
                lnkNext.Visible = false;
                lblNoGroups.Visible = false;

                try
                {
                    startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

                try
                {
                    count = Int32.Parse(Request.Params.Get("count"));
                }
                catch (ArgumentNullException)
                {
                    count = 10;
                }

                IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
                IGroupService groupService = iocManager.Resolve<IGroupService>();

                List<GroupWithNumberOfMembers> groups = groupService.GetAllGroups(startIndex, count);

                if (groups.Count == 0)
                {
                    lblNoGroups.Visible = true;
                    return;
                }

                this.gvGroups.DataSource = groups;
                this.gvGroups.DataBind();

                /* "Previous" link */
                if ((startIndex - count) >= 0)
                {
                    String url = "~/Pages/Group/AllGroups.aspx?startIndex=" + (startIndex - count) + "&count=" + count;

                    this.lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkPrevious.Visible = true;
                }

                /* "Next" link */
                if (groups.Count == count)
                {
                    String url = "~/Pages/Group/AllGroups.aspx?startIndex=" + (startIndex + count) + "&count=" + count;

                    this.lnkNext.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkNext.Visible = true;
                }
            }
        }

        protected void btnJoinGroup_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            long groupId = Convert.ToInt64(btn.CommandArgument);
            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IGroupService groupService = iocManager.Resolve<IGroupService>();

            try
            {
                groupService.JoinGroup(userId, groupId);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                lblNoGroups.Visible = true;
            }
        }

        protected void gvGroups_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton joinButton = e.Row.Cells[3].FindControl("btnJoinGroup") as LinkButton;

                    if (Context.User.Identity.IsAuthenticated)
                    {
                        var userSession = SessionManager.GetUserSession(Context);
                        if (userSession != null)
                        {
                            string loggedInUserName = userSession.LoginName;
                            long userId = userSession.UserProfileId;
                            long groupId = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "groupId"));

                            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
                            IGroupService groupService = iocManager.Resolve<IGroupService>();

                            bool isMember = groupService.IsUserMemberOfGroup(userId, groupId);

                            joinButton.Visible = !isMember;
                        }
                        else
                        {
                            joinButton.Visible = false;
                        }
                    }
                    else
                    {
                        joinButton.Visible = false;
                    }
                }
            }
        }
    }
