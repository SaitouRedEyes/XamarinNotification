using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace XamarinNotification
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private int count;
        private NotificationChannel nc;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            count = 1;

            Button btnMandaNotificacao = FindViewById<Button>(Resource.Id.btnMandaNotificacao);

            btnMandaNotificacao.Click += OnButtonSendNotificationClicked;
        }

        private void OnButtonSendNotificationClicked(object sender, EventArgs e)
        {
            // Configurando a Activity que será inicializada se o usuário clicar na notificação
            Intent resultIntent = new Intent(this, typeof(NotificationResultActivity));

            Bundle myParameters = new Bundle();
            myParameters.PutInt("count", count);
            
            resultIntent.PutExtras(myParameters);

            // Construct a back stack for cross-task navigation:
            Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(NotificationResultActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            //Build Notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, "location_notification")
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("Button Clicked")
                .SetNumber(count)
                .SetSmallIcon(Resource.Drawable.notification_template_icon_bg)
                .SetContentText(string.Format("The button has been clicked {0} times.", count));

            NotificationManager nf = (NotificationManager)GetSystemService(NotificationService);

            //Creating a Channel
            if (Build.VERSION.SdkInt > BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                string channelName = "Meu Canal de Notificação";
                string channelDescription = "Minha descrição do canal";
                nc = new NotificationChannel("location_notification", channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                nf.CreateNotificationChannel(nc);
            }

            //Send Notification
            NotificationManagerCompat nmc = NotificationManagerCompat.From(this);
            nmc.Notify(1000, builder.Build());

            count++;
        }    

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}