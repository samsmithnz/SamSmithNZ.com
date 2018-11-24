using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSNZ.DevOps.Web.Models
{
    public class DevOpsMenu2ViewModel
    {
        public DevOpsMenu2ViewModel(DevOpsMenuViewModel mainMenu, DevOpsMenuViewModel codeMenu, DevOpsMenuViewModel ciMenu, DevOpsMenuViewModel cdMenu, DevOpsMenuViewModel monitorMenu)
        {
            MainMenu = mainMenu;
            CodeMenu = codeMenu;
            CIMenu = ciMenu;
            CDMenu = cdMenu;
            MonitorMenu = monitorMenu;
        }

        public DevOpsMenuViewModel MainMenu { get; set; }
        public DevOpsMenuViewModel CodeMenu { get; set; }
        public DevOpsMenuViewModel CIMenu { get; set; }
        public DevOpsMenuViewModel CDMenu { get; set; }
        public DevOpsMenuViewModel MonitorMenu { get; set; }
    }
}