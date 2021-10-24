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
            SaveState();
        }

        protected override void OnResume()
        {
        }

        public async static Task<bool> LoadState()
        {
            CreatureData cd = await remoteStore.ReadItem();
            if (cd != null)
            {
                CurState = CurrentState.FromCreatureData(cd);

                //This fixes an issue that wouldn't exist if the people who programmed xamarin Entry fields had any sort of functioning brain
                if (CurState.Name != Preferences.Get("Name", "PLACEHOLDER"))
                {
                    CurState.Name = Preferences.Get("Name", "PLACEHOLDER II");
                }

                return true;
            }
            else
            {

                if (!Preferences.ContainsKey("CurrentState"))
                {
                    CurState = new CurrentState()
                    {
                        CurrentBoredState = BoredState.Satisfied,
                        CurrentDrinkState = DrinkState.Could_Drink,
                        CurrentEnergyState = EnergyState.Energized,
                        CurrentFoodState = FoodState.Peckish,
                        CurrentSocialState = SocialState.Fine,
                    };
                    Console.WriteLine("Made new stats file because none existed");
                    
                    return false;
                }
                CurState = JsonConvert.DeserializeObject<CurrentState>(Preferences.Get("CurrentState", null));
                return true;
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

        public static void DEBUGSetAlarmsToTenSecondsAgo()
        {
            AlarmManager am = AlarmManager.LoadAlarms();
            am.DEBUGSetAlarmsToTenSecondsAgo();
        }

    }
}
