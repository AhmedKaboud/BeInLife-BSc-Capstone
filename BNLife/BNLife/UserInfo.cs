using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Net;


namespace BNLife
{
    class UserInfo
    {
        public string GetUserID(string access_token) {
            FacebookClient fb = new FacebookClient(access_token);
            dynamic result = fb.Get("/me");
            return result.id;
        }

       public string GetUserName(string access_token)
        {
            FacebookClient fb = new FacebookClient(access_token);
            dynamic result = fb.Get("/me");
            return result.name;
        }

    }
}
