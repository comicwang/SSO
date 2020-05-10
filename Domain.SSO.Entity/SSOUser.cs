using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.SSO.Entity
{
    public class SSOUser
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Account { get; set; }

        public DateTime RegistDate { get; set; }

        public DateTime ModyfyDate { get; set; }

        public string Role { get; set; }

        public string Department { get; set; }

        public string Orgin { get; set; }

    }
}
