using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using TamaMossy.Models;
using TamaMossy.Views;
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

        public App()
        {
            InitializeComponent();

            remoteStore = new RemoteCreatureStore();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnSleep()
        {
           // timer.Stop();
            SaveState();
        }

        protected override void OnResume()
        {
            ////await LoadState();
            //CurState.GenerateNewIdleAnimation();
            //alarmManager = AlarmManager.LoadAlarms();

            //timer = new Timer { AutoReset = true, Interval = 1000 * 60 * 15 };
            //timer.Elapsed += TimerElapsed;
            //timer.Start();
        }

        public async static Task LoadState()
        {
            CreatureData cd = await remoteStore.ReadItem();
            if (cd != null)
            {
                CurState = CurrentState.FromCreatureData(cd);
            }
            else
            {

                if (!Preferences.ContainsKey("CurrentState"))
                {
                    CurState = new CurrentState();
                    Console.WriteLine("Made new stats file because none existed");
                    SaveState();
                    return;
                }
                CurState = JsonConvert.DeserializeObject<CurrentState>(Preferences.Get("CurrentState", null));
            }

        }

        public static void SaveState()
        {
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
