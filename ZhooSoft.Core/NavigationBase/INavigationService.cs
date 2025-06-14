﻿using CommunityToolkit.Maui.Views;
using ZTaxi.Model.DTOs.UserApp;

namespace ZhooSoft.Core.NavigationBase
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task PushAsync(Page page, Dictionary<string, object> navigationParams);
        Task PushModalAsync(Page page);
        Task PopAsync();

        Task<object?> OpenPopupAsync(Popup popup);

        void OpenPopup(Popup popup);

        Task<object> OpenPopup(Popup popup, Dictionary<string, object> navigationParams);

        Task ClosePopup();

        Task PopAsync(Dictionary<string, object> NavigationParams);

        

        Task PopToRootAsync();

        //ViewModelBase GetCurrentPageModel();

        Task ClosePopup(Dictionary<string, object> navigationParams, bool IsBackPopup = false);

        Task OnTabClicked(object obj);
    }
}
