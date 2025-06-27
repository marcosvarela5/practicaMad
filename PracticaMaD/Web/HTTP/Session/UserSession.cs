using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {

        private long userProfileId;
        private String firstName;
        private String loginName;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }
        
        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
    }
}


