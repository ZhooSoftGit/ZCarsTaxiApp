using Android.App;
using Android.Runtime;

namespace ZCarsDriver
{
    [Application]
    public class MainApplication : MauiApplication
    {
        #region Constructors

        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        #endregion

        #region Methods

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        #endregion
    }
}
