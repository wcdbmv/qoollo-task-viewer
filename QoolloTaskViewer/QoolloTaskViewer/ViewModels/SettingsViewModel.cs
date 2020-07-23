using QoolloTaskViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.ViewModels
{
    public class SettingsViewModel
    {
        public TokenViewModel TokenToAdd { get; set; }
        public List<TokenViewModel> Tokens { get; set; }
        public UserModel User { get; set; }
    }
}
