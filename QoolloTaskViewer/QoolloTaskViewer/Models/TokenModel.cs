using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Models
{
    public class TokenModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public string InServiceUsername { get; set; }
        public string Token { get; set; }
        public bool Enabled { get; set; }

        public UserModel User { get; set; }
        public ServiceModel Service { get; set; }
    }
}
