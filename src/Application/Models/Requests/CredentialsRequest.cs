using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class CredentialsRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public RolEnum Rol { get; set; }
    }
}
