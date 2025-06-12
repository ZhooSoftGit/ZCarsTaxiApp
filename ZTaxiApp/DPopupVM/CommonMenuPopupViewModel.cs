using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Core;

namespace ZTaxiApp.DPopupVM
{
    public class CommonMenuPopupViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Menu";
        public string Message { get; set; } = "Choose an option";

        public ICommand CloseCommand { get; }

        public CommonMenuPopupViewModel(Func<Task> closeAction)
        {
            CloseCommand = new Command(async () => await closeAction());
        }
    }
}
