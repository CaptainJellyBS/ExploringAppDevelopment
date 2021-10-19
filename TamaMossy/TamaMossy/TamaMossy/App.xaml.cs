using Newtonsoft.Json;
using System;
using System.IO;
using System.Timers;
using TamaMossy.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TamaMossy
{
    public partial class App : Application
    {
        INotificationManager notificationManager;

        public static CurrentState CurState {get; private set;}
        static RemoteCreatureStore remoteStore;
        public AlarmManager alarmManager;
        Timer timer;

        public App()
        {
            InitializeComponent();
            LoadState(); //Putting this in just the OnStart and OnResume made the app crash :/
            CurState.GenerateNewIdleAnimation(); //Kinda same as the above, MainPage would be loaded before the idle animation was generated

            remoteStore = new RemoteCreatureStore();
            MainPage = new AppShell();

            notificationManager = DependencyService.Get<INotificationManager>();

            notificationManager.StartAlarmCycle();
        }

        protected override void OnStart()
        {
            LoadState();
            alarmManager = AlarmManager.LoadAlarms();
            timer = new Timer { AutoReset = true, Interval = 1000 * 60 };

            timer.Elapsed += TimerElapsed;
            timer.Start();

            //Debug Stuff, remove later
            Preferences.Set("PlayerName", "Mana TEST PLACEHOLDER");
            CurState.Name = "TAMAMOSSY TEST PLACEHOLDER";
        }


        protected override void OnSleep()
        {
            timer.Stop();
            SaveState();
        }

        protected override void OnResume()
        {
            LoadState();
            CurState.GenerateNewIdleAnimation();
            alarmManager = AlarmManager.LoadAlarms();

            timer = new Timer { AutoReset = true, Interval = 1000 * 60 };
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            alarmManager.UpdateTimers();
        }

        public static void LoadState()
        {
            if(!Preferences.ContainsKey("CurrentState"))
            {
                CurState = new CurrentState();
                Console.WriteLine("Made new stats file because none existed");
                SaveState();
                return;
            }

            CurState = JsonConvert.DeserializeObject<CurrentState>(Preferences.Get("CurrentState", null));
        }

        public static void SaveState()
        {
            //File.WriteAllText(Path.Combine(App.FolderPath, filename), CurState.ParseToString());
            Preferences.Set("CurrentState", JsonConvert.SerializeObject(CurState));
            if (Preferences.Get("ID", 0) == 0)
            {
                remoteStore.CreateItem(CurState.ToCreatureData());
            }
            else
            {
                remoteStore.UpdateItem(CurState.ToCreatureData());
            }
        }

        public static void UpdateAlarms()
        {
            AlarmManager am = AlarmManager.LoadAlarms();
            am.UpdateTimers();
        }

    }
}
