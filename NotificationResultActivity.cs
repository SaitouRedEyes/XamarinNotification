using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinNotification
{
    [Activity(Label = "NotificationResultActivity")]
    public class NotificationResultActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            int count = Intent.Extras.GetInt("count", -1);

            // No count was passed? Then just return.
            if (count < 0)
                return;

            Toast.MakeText(this, count.ToString() + " Notifications", ToastLength.Short).Show();
        }
    }
}