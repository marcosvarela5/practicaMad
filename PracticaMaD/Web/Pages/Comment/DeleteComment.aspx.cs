using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class DeleteComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnDeleteCommentClick(object sender, EventArgs e)
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
                    commentService.DeleteComment(commentId.Value, SessionManager.GetUserSession(Context).UserProfileId);

                    Response.Redirect("~/Pages/MainPage.aspx");
                }
                catch (InstanceNotFoundException)
                {

                }

            }

        }
    }
}