using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.Core.Alerts
{
    public interface IAlertService
    {
        Task ShowAlert(string title, string message, string cancel);
        Task<bool> ShowConfirmation(string title, string message, string accept, string cancel);
    }
}
