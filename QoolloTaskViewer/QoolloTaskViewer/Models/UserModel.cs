using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
