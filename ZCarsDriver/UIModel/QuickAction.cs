using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCarsDriver.Helpers;

namespace ZCarsDriver.UIModel
{
    public class QuickAction
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public ActionEnum Action { get; set; }
    }
}
