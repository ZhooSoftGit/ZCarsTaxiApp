﻿using CommunityToolkit.Mvvm.Input;
using ZTaxiApp.DPopup;
using ZTaxiApp.Helpers;
using ZhooSoft.Core;

namespace ZTaxiApp.DPopupVM
{
    public partial class ActionPopupViewModel : ViewModelBase
    {
        internal ActionPopup CurrentPopup;

        #region Constructors

        public ActionPopupViewModel()
        {
            ShowProfileCommand = new AsyncRelayCommand(ShowProfile);
            ShowDashBoardCommand = new AsyncRelayCommand(ShowDashBoard);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand ShowDashBoardCommand { get; }

        public IAsyncRelayCommand ShowProfileCommand { get; }

        #endregion

        #region Methods

        private async Task ShowDashBoard()
        {
            await CurrentPopup.CloseAsync(PopupEnum.ShowDashboard);
        }

        private async Task ShowProfile()
        {
            await CurrentPopup.CloseAsync(PopupEnum.ShowProfile);
        }

        #endregion
    }
}
