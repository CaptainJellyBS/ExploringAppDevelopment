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
        public string drink { get; set; }
        public string energy { get; set; }
        public string social { get; set; }
        public string bored { get; set; }
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
                drink = curState.currentDrinkState.ToString().Replace('_', ' ');
                drinkText.Text = drink;
                energy = curState.currentEnergyState.ToString().Replace('_', ' ');
                energyText.Text = energy;
                social = curState.currentSocialState.ToString().Replace('_', ' ');
                socialText.Text = social;
                bored = curState.currentBoredState.ToString().Replace('_', ' ');
                boredText.Text = bored;
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
        
        void OnDrinkIncreaseClicked(object sender, EventArgs args)
        {
            curState.currentDrinkState = (DrinkState)Math.Min((int)curState.currentDrinkState + 1, (int)DrinkState.Slaked);
            SaveState();
            drink = curState.currentDrinkState.ToString().Replace('_', ' ');
            drinkText.Text = drink;
        }

        void OnDrinkDecreaseClicked(object sender, EventArgs args)
        {
            curState.currentDrinkState = (DrinkState)Math.Max((int)curState.currentDrinkState - 1, (int)DrinkState.Dehydrated);
            SaveState();
            drink = curState.currentDrinkState.ToString().Replace('_', ' ');
            drinkText.Text = drink;
        } 
        
        void OnEnergyIncreaseClicked(object sender, EventArgs args)
        {
            curState.currentEnergyState = (EnergyState)Math.Min((int)curState.currentEnergyState + 1, (int)EnergyState.Energized);
            SaveState();
            energy = curState.currentEnergyState.ToString().Replace('_', ' ');
            energyText.Text = energy;
        }

        void OnEnergyDecreaseClicked(object sender, EventArgs args)
        {
            curState.currentEnergyState = (EnergyState)Math.Max((int)curState.currentEnergyState - 1, (int)EnergyState.Exhausted);
            SaveState();
            energy = curState.currentEnergyState.ToString().Replace('_', ' ');
            energyText.Text = energy;
        }
        
        void OnSocialIncreaseClicked(object sender, EventArgs args)
        {
            curState.currentSocialState = (SocialState)Math.Min((int)curState.currentSocialState + 1, (int)SocialState.Panicking);
            SaveState();
            social = curState.currentSocialState.ToString().Replace('_', ' ');
            socialText.Text = social;
        }

        void OnSocialDecreaseClicked(object sender, EventArgs args)
        {
            curState.currentSocialState = (SocialState)Math.Max((int)curState.currentSocialState - 1, (int)SocialState.Forlorn);
            SaveState();
            social = curState.currentSocialState.ToString().Replace('_', ' ');
            socialText.Text = social;
        }

        void OnBoredIncreaseClicked(object sender, EventArgs args)
        {
            curState.currentBoredState = (BoredState)Math.Min((int)curState.currentBoredState + 1, (int)BoredState.Satisfied);
            SaveState();
            bored = curState.currentBoredState.ToString().Replace('_', ' ');
            boredText.Text = bored;
        }

        void OnBoredDecreaseClicked(object sender, EventArgs args)
        {
            curState.currentBoredState = (BoredState)Math.Max((int)curState.currentBoredState - 1, (int)BoredState.Bored);
            SaveState();
            bored = curState.currentBoredState.ToString().Replace('_', ' ');
            boredText.Text = bored;
        }

        void SaveState()
        {
            File.WriteAllText(Path.Combine(App.FolderPath, filename), curState.ParseToString());
        }
    }
}