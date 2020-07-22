using QoolloTaskViewer.ApiServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public string Domain { get; set; }
        public string InServiceUsername { get; set; }
        public ServiceType Type { get; set; }
        public string Username { get; set; }
    }
}
