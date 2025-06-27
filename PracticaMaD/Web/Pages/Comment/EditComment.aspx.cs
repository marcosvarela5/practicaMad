using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class EditComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCommentOkey.Visible = false;

            lblNotOwner.Visible = false;

        }

        protected void BtnEditCommentClick(object sender, EventArgs e)
        {
            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];
            ICommentTableService commentService = ioCManager.Resolve<ICommentTableService>();

            long? commentId;
            try
            {
                commentId = Int64.Parse(Request.Params.Get("commId").ToString());
            }
            catch (NullReferenceException)
            {
                commentId = null;
            }
            if (commentId.HasValue)
            {
                try
                {
                    commentService.UpdateComment(commentId.Value, SessionManager.GetUserSession(Context).UserProfileId, txtComment.Text);

                    lblCommentOkey.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    lblNotOwner.Visible = true;
                }
                catch (Exception)
                {
                    lblNotOwner.Visible = true;
                    lblCommentOkey.Visible = false;
                }
            }
        }
    }
}