using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.CommentTableService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class AddComment : SpecificCulturePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!SessionManager.IsUserAuthenticated(Context)){
                    Response.Redirect("~/Pages/User/Authentication.aspx");
                }

                lblCommentOkey.Visible = false;
                long? eventId;
                try
                {
                    eventId = Int64.Parse(Request.Params.Get("eventId").ToString());
                }
                catch (NullReferenceException)
                {
                    eventId = null;
                }
            }

           

        }

        protected void BtnAddCommentClick(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                lblCommentOkey.Visible = true;
                return;
            }

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
           ICommentTableService commentService = iocManager.Resolve<ICommentTableService>();

            long? eventId;
            try
            {
                eventId = Int64.Parse(Request.Params.Get("eventId").ToString());
            }
            catch (NullReferenceException)
            {
                eventId = null;
            }
            if (eventId.HasValue)
            {
                try
                {
                    long commentId = commentService.AddComment(eventId.Value, SessionManager.GetUserSession(Context).UserProfileId, txtComment.Text);
                    lblCommentOkey.Visible = true;
                    Response.Redirect("~/Pages/Event/FindEvents.aspx");
                }
                catch (InstanceNotFoundException)
                {
                    lblCommentOkey.Visible = false;
                }
            }
        }

     
    }
}