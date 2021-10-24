using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamaMossy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    //Code taken from: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/local-notifications

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationTestPage : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;

        public NotificationTestPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void OnSendClick(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(new NotificationEventArgs() { Title = title, Message = message });
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            App.UpdateAlarms();
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }
    }
}