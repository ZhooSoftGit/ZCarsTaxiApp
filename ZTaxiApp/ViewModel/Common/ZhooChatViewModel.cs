using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Services;
using ZTaxiApp.UIModel;

namespace ZTaxiApp.ViewModel
{
    public partial class ZhooChatViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string messageText;

        public ObservableCollection<ChatMessage> Messages { get; } = new();

        public ObservableCollection<string> QuickMessages { get; } = new()
    {
        "Are you coming ❓",
        "Waiting at pickup 📍",
        "My location is as per map 🗺️",
        "Message when reached 💬"
    };

        public ICommand SendMessageCommand => new Command(SendManualMessage);
        public ICommand SendQuickMessageCommand => new Command<string>(SendQuickMessage);

        private UserSignalRService _userSignalRService;

        public HubConnection _hub;

        public ZhooChatViewModel()
        {
            PageTitleName = "Message"
            _userSignalRService = ServiceHelper.GetService<UserSignalRService>();
            _hub = _userSignalRService.GetConnection();
            _hub.On<string>("messagefromdriver", OnDriverMessageReceived);
        }

        private void OnDriverMessageReceived(string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(new ChatMessage
                {
                    Text = message,
                    IsIncoming = true,
                    Time = DateTime.Now
                });
            });
        }

        private void SendManualMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText)) return;

            SendMessage(MessageText);
            MessageText = string.Empty;
        }

        private void SendQuickMessage(string msg)
        {
            SendMessage(msg);
        }

        private void SendMessage(string message)
        {
            try
            {
                Messages.Add(new ChatMessage
                {
                    Text = message,
                    IsIncoming = false,
                    Time = DateTime.Now
                });

                _hub.SendAsync("sendmessagetodriver", message); // Change to your actual server method
            }
            catch(Exception ex)
            {

            }
            OnDriverMessageReceived("Ok na");

        }
    }

    
}
