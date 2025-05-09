using ZhooSoft.Core;
using ZTaxiApp.Views.Common;

namespace ZTaxiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Light;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(ServiceHelper.GetService<SplashScreen>());
        }
    }
}