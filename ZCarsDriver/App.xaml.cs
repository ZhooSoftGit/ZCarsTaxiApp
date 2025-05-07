using ZCarsDriver.Views;
using ZCarsDriver.Views.Common;
using ZCarsDriver.Views.Driver;
using ZhooSoft.Auth;
using ZhooSoft.Auth.Views;
using ZhooSoft.Core;

namespace ZCarsDriver
{
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Light;
        }

        #endregion

        #region Methods

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(ServiceHelper.GetService<SplashScreen>());
        }

        private void CheckLogin()
        {
        }

        #endregion
    }
}
