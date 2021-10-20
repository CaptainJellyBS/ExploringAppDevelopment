using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    //Code taken from: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/local-notifications
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        //void SendNotification(string title, string message, DateTime? notifyTime = null);
        void SendNotification(NotificationEventArgs args, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);
        void StartAlarmCycle();

    }
}
