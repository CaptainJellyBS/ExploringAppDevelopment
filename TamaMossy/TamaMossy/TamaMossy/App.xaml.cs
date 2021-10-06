using System;
using System.IO;
using TamaMossy.Models;
using Xamarin.Forms;

namespace TamaMossy
{
    public partial class App : Application
    {
        static string filename = "current_state.txt";
        public static string FolderPath { get; private set; }
        public static CurrentState CurState {get; private set;}

        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new AppShell();
            LoadState();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
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
