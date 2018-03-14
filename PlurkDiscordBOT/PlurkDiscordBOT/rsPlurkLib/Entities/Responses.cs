using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace RenRen.Plurk.Entities
{
    public class SetFollowing
    {
        public string success_text { get; set; }
        public string error_text { get; set; }
    }
    public class GetPlurkResponse
    {
        public Plurk plurk { get; set; }
        public User user { get; set; }
        
    }
    public class GetUsers
    {
        public User[] users { get; set; }
    }
    public class GetPlurksResponse
    {
        public Plurk[] plurks { get; set; }
       
        public Dictionary<int, User> plurk_users { get; set; }

    }
    public class GetPublicProfiles
    {
        public UserInfo user_info { get; set; }
    }

    public class GetResponseResponse
    {
        public Dictionary<int, User> friends { get; set; }
        public int responses_seen { get; set; }
        public Response[] responses { get; set; }
    }

    public class ErrorResponse
    {
        public string error_text { get; set; }
    }
}
