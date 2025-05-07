using Microsoft.Maui.Handlers;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;



#if ANDROID
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
#endif
#if IOS || MACCATALYST
using UIKit;
#endif

namespace ZhooSoft.Controls
{
    public class CustomEntryHandler : EntryHandler
    {

#if ANDROID

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            var border = new GradientDrawable();
            border.SetStroke(4, Android.Graphics.Color.Gray); // Border thickness and color
            border.SetCornerRadius(12f); // Rounded corners
            platformView.SetBackground(border);
        }

#endif

#if IOS

        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Layer.BorderWidth = 2;
            platformView.Layer.BorderColor = UIColor.Gray.CGColor;
            platformView.Layer.CornerRadius = 8;
        }

#endif

    }
}
