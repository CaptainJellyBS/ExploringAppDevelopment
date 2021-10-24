using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TamaMossy.Models;
using System.Timers;
using Xamarin.Essentials;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string mossImage;
        private INotificationManager notificationManager;
        private AlarmManager alarmManager;
        private Timer timer;
        bool loaded;

        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();
            loaded = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = this;

            await Init();
            MossImage = SpriteCalculator.CalculateAnimationPath();
            loaded = true;
        }

        async Task Init()
        {
            bool hasName = await App.LoadState();

            App.CurState.GenerateNewIdleAnimation(); 
            alarmManager = AlarmManager.LoadAlarms();            

            if(!hasName)
            {
                alarmManager.TimerInitialize();
                alarmManager.SaveAlarms();

                Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new NamesPage()));
                hasName = true;
            }

            notificationManager = DependencyService.Get<INotificationManager>();

            notificationManager.StartAlarmCycle();

            timer = new Timer { AutoReset = true, Interval = 1000 * 60 * 15 };
            timer.Elapsed += TimerElapsed;
            timer.Start();

        }
        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            App.UpdateAlarms();
        }

        void DebugStatsButtonClicked(object sender, EventArgs e)
        {
            if (!loaded) { return; }
            Navigation.PushAsync(new DEBUGStatPage());
        }

        void StatsButtonClicked(object sender, EventArgs e)
        {
            if (!loaded) { return; }

            Navigation.PushAsync(new StatPage());
        }

        void TestNotiPage(object sender, EventArgs e)
        {
            if (!loaded) { return; }

            Navigation.PushAsync(new NotificationTestPage());
        }

        void ParkPageClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ParkPage());
        }
    }
}