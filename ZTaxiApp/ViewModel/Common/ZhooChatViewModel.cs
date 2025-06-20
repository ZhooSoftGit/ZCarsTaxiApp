using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services;
using ZTaxiApp.UIModel;

namespace ZTaxiApp.ViewModel
{
    public partial class ZhooChatViewModel : ViewModelBase
    {
        #region Fields

        public HubConnection _hub;

        private UserSignalRService _userSignalRService;

        [ObservableProperty]
        private string messageText;

        #endregion

        #region Constructors

        public ZhooChatViewModel()
        {
            PageTitleName = "Message";
            _userSignalRService = ServiceHelper.GetService<UserSignalRService>();
            _userSignalRService.OnMessageReceived += _userSignalRService_OnMessageReceived;
        }

        #endregion

        #region Properties
        [ObservableProperty]
        private ObservableCollection<ChatMessage> _messages;

        public ObservableCollection<string> QuickMessages { get; } = new()
                                {
                                    "Are you coming ❓",
                                    "Waiting at pickup 📍",
                                    "My location is as per map 🗺️",
                                    "Message when reached 💬"
                                };

        public ICommand SendMessageCommand => new Command(SendManualMessage);

        public ICommand SendQuickMessageCommand => new Command<string>(SendQuickMessage);

        #endregion

        #region Methods

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (AppHelper.ChatMessages != null)
            {
                var messages = AppHelper.ChatMessages.ToList();
                Messages = new ObservableCollection<ChatMessage>(messages);
            }
        }

        private void _userSignalRService_OnMessageReceived(string obj)
        {
            OnDriverMessageReceived(obj);
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

        private void SendMessage(string message)
        {
            try
            {
                var chatmessage = new ChatMessage
                {
                    Text = message,
                    IsIncoming = false,
                    Time = DateTime.Now
                };
                AppHelper.ChatMessages.Add(chatmessage);
                Messages.Add(chatmessage);

                _userSignalRService.SendMessageToDriver(message);
            }
            catch (Exception ex)
            {

            }
            OnDriverMessageReceived("Ok na");
        }

        private void SendQuickMessage(string msg)
        {
            SendMessage(msg);
        }

        #endregion
    }
}
