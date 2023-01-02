using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationJwt.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() 
            {
                UserName = "BenJohn",
                Password = "Benny",
                EmailAddress ="BenJohn10@gmail.com",
                GivenName = "John",
                Surname = "Benjamin",
                Role = "Admin"
            },
            new UserModel()
            {
                UserName = "Babatunde",
                Password = "Babatunde",
                EmailAddress ="Babatunde@gmail.com",
                GivenName = "Funmi",
                Surname = "Babatunde",
                Role = "Seller"
            },
        };
    }
}
