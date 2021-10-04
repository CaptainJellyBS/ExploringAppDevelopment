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
        public string food { get; set; }
        public StatPage()
        {
            BindingContext = this;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (File.Exists(Path.Combine(App.FolderPath, filename)))
            {
                curState = CurrentState.ParseFromString(File.ReadAllText(Path.Combine(App.FolderPath, filename)));
                food = curState.currentFoodState.ToString().Replace('_', ' ');
                foodText.Text = food;

                Console.WriteLine(food);
            }
            else
            {
                curState = new CurrentState();
                SaveState();
            }
        }
        
        void OnFoodIncreaseClicked(object sender, EventArgs args)
        {
            curState.currentFoodState = (FoodState)Math.Min((int)curState.currentFoodState + 1, (int)FoodState.Stuffed);
            SaveState();
            food = curState.currentFoodState.ToString().Replace('_', ' ');
            foodText.Text = food;
        }

        void OnFoodDecreaseClicked(object sender, EventArgs args)
        {
            curState.currentFoodState = (FoodState)Math.Max((int)curState.currentFoodState - 1, (int)FoodState.Starving);
            SaveState();
            food = curState.currentFoodState.ToString().Replace('_', ' ');
            foodText.Text = food;
        }

        void SaveState()
        {
            File.WriteAllText(Path.Combine(App.FolderPath, filename), curState.ParseToString());
        }
    }
}