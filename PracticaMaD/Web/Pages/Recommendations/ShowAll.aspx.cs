using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendations
{
    public partial class ShowAll : SpecificCulturePage
    {

        private long userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkPrevious.Visible = false;
            int startIndex, count;

            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 10;
            }

            if (!SessionManager.IsUserAuthenticated(Context))
            {
                RedirectToAuthentication();
                return;
            }

            userId = SessionManager.GetUserSession(Context).UserProfileId;

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationService recommendationService = iocManager.Resolve<IRecommendationService>();

            RecBlock recommendations = recommendationService.FindRecommendations(userId, startIndex, count);

            if (recommendations.recommendations.Count == 0)
            {
                lblNoRecomendations.Visible = true;
                return;
            }

            recommendationGrid.DataSource = recommendations.recommendations;
            recommendationGrid.DataBind();

            if ((startIndex - count) >= 0)
            {

                string url = "~/Pages/Recommendations/ShowAll.aspx?startIndex=" + (startIndex - count) + "&count=" + count;
                this.lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;

            }

            if (recommendations.existsMore)
            {
                string url = "~/Pages/Recommendations/ShowAll.aspx?startIndex=" + (startIndex + count) + "&count=" + count;
                lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        private void RedirectToAuthentication()
        {
            string url = "./Pages/User/Authentication.aspx";
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }


        protected void GridView_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RecData r = (RecData)e.Row.DataItem;
                if (r.hasComments)
                {
                    LinkButton lb = new LinkButton
                    {
                        Text = r.eventName,
                        CommandArgument = r.eventId.ToString(),
                        CommandName = "comentarios"
                    };
                    lb.Command += LinkButton_Command;
                    e.Row.Cells[3].Controls.Add(lb);
                }
            }
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "comentarios")
            {
                string url = "~/Pages/Comment/AllComments.aspx?eventId=" + Convert.ToInt64(e.CommandArgument);

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}