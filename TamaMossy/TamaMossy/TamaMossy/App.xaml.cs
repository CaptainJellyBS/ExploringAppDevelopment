using System;
using System.IO;
using System.Timers;
using TamaMossy.Models;
using Xamarin.Forms;

namespace TamaMossy
{
    public partial class App : Application
    {
        static string filename = "current_state.txt";
        public static string FolderPath { get; private set; }
        public static CurrentState CurState {get; private set;}
        public AlarmManager alarmManager;
        Timer timer;

        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new AppShell();
            LoadState();
        }

        protected override void OnStart()
        {
            alarmManager = AlarmManager.LoadAlarms();
            timer = new Timer { AutoReset = true, Interval = 1000 * 60 };
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }


        protected override void OnSleep()
        {
            timer.Stop();
        }

        protected override void OnResume()
        {
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
            if (File.Exists(Path.Combine(App.FolderPath, filename)))
            {
                CurState = CurrentState.ParseFromString(File.ReadAllText(Path.Combine(FolderPath, filename)));
            }
            else
            {
                CurState = new CurrentState();
                Console.WriteLine("Made new stats file because none existed");
                SaveState();
            }
        }

        public static void SaveState()
        {
            File.WriteAllText(Path.Combine(App.FolderPath, filename), CurState.ParseToString());
        }
    }
}
