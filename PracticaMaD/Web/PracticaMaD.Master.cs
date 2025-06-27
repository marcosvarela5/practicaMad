using System;

using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{

    public partial class PracticaMaD : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = true;
                if(lblDash5 != null)
                    lnkMyGroups.Visible = false;
                if(lblDash6 != null)
                    lblDash6.Visible = true;
                if(lblDash7 != null)
                    lnkAddGroup.Visible = false;
                if(lblDash8 != null)
                    lnkShowRecommendations.Visible = false;
            }
            else
            {
                if (lblWelcome != null)                   
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = true;
                if (lnkAllGroups != null)
                    lnkAllGroups.Visible = true;
                if(lblDash5 != null)
                    lblDash5.Visible = true;
                if (lnkMyGroups != null)
                    lnkMyGroups.Visible = true;
                if (lblDash6 != null)
                    lblDash6.Visible = true;
                if(lnkFindEvents != null)
                    lnkFindEvents.Visible = true;
                if(lblDash7!= null)
                    lblDash7.Visible = true;
                if(lnkAddGroup != null)
                    lnkAddGroup.Visible = true;
                if (lblDash8 != null)
                    lblDash8.Visible = true;
                if (lnkShowRecommendations != null)
                    lnkShowRecommendations.Visible = true;
            }
        }
    }
}
