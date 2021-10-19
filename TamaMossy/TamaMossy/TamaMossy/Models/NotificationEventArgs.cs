using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    //Code taken from: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/local-notifications

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
