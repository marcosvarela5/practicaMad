using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class AllComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int startIndex, count;
                lnkPrevious.Visible = false;
                lnkNext.Visible = false;
                lblNoComments.Visible = false;

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

                long? eventId;
                try
                {
                    eventId = Int64.Parse(Request.Params.Get("eventId"));
                }
                catch (NullReferenceException)
                {
                    eventId = null;
                }

                if (eventId.HasValue)
                {
                    IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
                    ICommentTableService commentService = iocManager.Resolve<ICommentTableService>();

                    List<CommentTable> comments = commentService.FindEventComments(eventId.Value, startIndex, count);

                    if (comments.Count == 0)
                    {
                        lblNoComments.Visible = true;
                        return;
                    }

                    this.gvComments.DataSource = comments;
                    this.gvComments.DataBind();

                    /* "Previous" link */
                    if ((startIndex - count) >= 0)
                    {
                        String url = "~/Pages/Event/AllComments.aspx?eventId=" + eventId.Value +
                            "&startIndex=" + (startIndex - count) + "&count=" + count;

                        this.lnkPrevious.NavigateUrl =
                            Response.ApplyAppPathModifier(url);
                        this.lnkPrevious.Visible = true;
                    }

                    /* "Next" link */
                    if (comments.Count == count)
                    {
                        String url = "~/Pages/Event/AllComments.aspx?eventId=" + eventId.Value +
                           "&startIndex=" + (startIndex + count) + "&count=" + count;

                        this.lnkNext.NavigateUrl =
                            Response.ApplyAppPathModifier(url);
                        this.lnkNext.Visible = true;
                    }
                }
            }
        }

        protected void gvComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink editLink = e.Row.FindControl("lnkEdit") as HyperLink;
                HyperLink deleteLink = e.Row.FindControl("lnkDelete") as HyperLink;

                if (editLink != null && deleteLink != null)
                {
                    if (Context.User.Identity.IsAuthenticated)
                    {
                        int commentId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "commId"));

                        if (isOwner(commentId))
                        {
                            editLink.Visible = true;
                            deleteLink.Visible = true;
                        }
                        else
                        {
                            editLink.Visible = false;
                            deleteLink.Visible = false;
                        }
                    }
                    else
                    {
                        editLink.Visible = false;
                        deleteLink.Visible = false;
                    }
                }
            }
        }

        protected bool isOwner(int commId)
        {
            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];
            ICommentTableService commentService = ioCManager.Resolve<ICommentTableService>();

            CommentTable comment = commentService.FindComment(commId);

            return comment.userId == SessionManager.GetUserSession(Context).UserProfileId;
        }
    }
}