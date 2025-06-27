using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Model.GroupService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendations
{
    public partial class Recommend : SpecificCulturePage
    {
        private long eventId, userId;
        private int startIndex = 0, count = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!SessionManager.IsUserAuthenticated(Context)){
                    Response.Redirect("~/Pages/User/Authentication.aspx");
                }
            }
            lblRecommendOkey.Visible = false;

            userId = SessionManager.GetUserSession(Context).UserProfileId;
            eventId = GetEventIdFromRequest();

            if (!IsPostBack)
            {
                InitializeGroupList();
            }
        }
        

        private long GetEventIdFromRequest()
        {
            if (long.TryParse(Request.Params.Get("eventId"), out long eventId))
            {
                return eventId;
            }
            throw new ArgumentException("Invalid event ID");
        }

        private void InitializeGroupList()
        {
            SetPaginationParameters();
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IGroupService groupService = iocManager.Resolve<IGroupService>();

            List<GroupWithNumberOfMembers> groupList = groupService.GetUserGroups(userId, startIndex, count);

            GroupList.DataSource = groupList;
            GroupList.DataTextField = "groupName";
            GroupList.DataValueField = "groupId";
            GroupList.DataBind();
        }

        private void SetPaginationParameters()
        {
            if (int.TryParse(Request.Params.Get("startIndex"), out int startIndex))
            {
                this.startIndex = startIndex;
            }

            if (int.TryParse(Request.Params.Get("count"), out int count))
            {
                this.count = count;
            }
        }

        protected void clickRecommend(object sender, EventArgs e)
        {
            

            string description = txtRecommendation.Text;
            List<long> selectedGroupIds = GetSelectedGroupIds();

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IRecommendationService recommendationService = iocManager.Resolve<IRecommendationService>();

            

            foreach (long groupId in selectedGroupIds)
            {
                
                recommendationService.RecommendEventToGroup(eventId, groupId, userId, description);
                Response.Redirect("~/Pages/Group/AllGroups.aspx");
            }

        }

        private List<long> GetSelectedGroupIds()
        {
            List<long> selectedGroupIds = new List<long>();
            foreach (ListItem item in GroupList.Items)
            {
                if (item.Selected)
                {
                    selectedGroupIds.Add(Convert.ToInt64(item.Value));
                }
            }
            return selectedGroupIds;
        }
    }
}