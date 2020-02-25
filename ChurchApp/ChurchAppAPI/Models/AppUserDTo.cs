using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class AppUserDTo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAuthenticated { get; set; }

        public List<string> UserClaims { get; set; }

        public string BearerToken { get; set; }
    }
}
