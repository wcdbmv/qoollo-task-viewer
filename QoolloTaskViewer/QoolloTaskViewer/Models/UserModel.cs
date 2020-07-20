using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QoolloTaskViewer.Models
{
    public class UserModel : IdentityUser
    {
        public List<TokenModel> Tokens { get; set; }
    }
}
