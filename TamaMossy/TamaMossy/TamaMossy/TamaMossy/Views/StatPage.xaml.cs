using System;
using System.IO;
using Xamarin.Forms;
using TamaMossy.Models;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        string filename = "current_state.txt";
        public CurrentState curState;
        public StatPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (File.Exists(Path.Combine(App.FolderPath, filename)))
            {
                curState = CurrentState.ParseFromString(File.ReadAllText(Path.Combine(App.FolderPath, filename)));
                //Load stats here
            }
            else
            {
                curState = new CurrentState();
                File.WriteAllText(Path.Combine(App.FolderPath, filename), curState.ParseToString());
            }
        }
    }
}