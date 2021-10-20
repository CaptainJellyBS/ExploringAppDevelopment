using System;
using System.IO;
using Xamarin.Forms;
using TamaMossy.Models;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DEBUGStatPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public CurrentState curState;
        public string food { get; set; }
        public string drink { get; set; }
        public string energy { get; set; }
        public string social { get; set; }
        public string bored { get; set; }

        public DEBUGStatPage()
        {
            BindingContext = this;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            curState = App.CurState;
            food = curState.CurrentFoodState.ToString().Replace('_', ' ');
            foodText.Text = food; 
            drink = curState.CurrentDrinkState.ToString().Replace('_', ' ');
            drinkText.Text = drink;
            energy = curState.CurrentEnergyState.ToString().Replace('_', ' ');
            energyText.Text = energy;
            social = curState.CurrentSocialState.ToString().Replace('_', ' ');
            socialText.Text = social;
            bored = curState.CurrentBoredState.ToString().Replace('_', ' ');
            boredText.Text = bored;
            MossImage = SpriteCalculator.CalculateAnimationPath();
        }
        
        void OnFoodIncreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentFoodState++;
            App.SaveState();
            food = curState.CurrentFoodState.ToString().Replace('_', ' ');
            foodText.Text = food;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnFoodDecreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentFoodState--;
            App.SaveState();
            food = curState.CurrentFoodState.ToString().Replace('_', ' ');
            foodText.Text = food;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnDrinkIncreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentDrinkState++;
            App.SaveState();
            drink = curState.CurrentDrinkState.ToString().Replace('_', ' ');
            drinkText.Text = drink;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnDrinkDecreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentDrinkState--;
            App.SaveState();
            drink = curState.CurrentDrinkState.ToString().Replace('_', ' ');
            drinkText.Text = drink;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnEnergyIncreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentEnergyState++;
            App.SaveState();
            energy = curState.CurrentEnergyState.ToString().Replace('_', ' ');
            energyText.Text = energy;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnEnergyDecreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentEnergyState--;
            App.SaveState();
            energy = curState.CurrentEnergyState.ToString().Replace('_', ' ');
            energyText.Text = energy;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnSocialIncreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentSocialState++;
            App.SaveState();
            social = curState.CurrentSocialState.ToString().Replace('_', ' ');
            socialText.Text = social;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnSocialDecreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentSocialState--;
            App.SaveState();
            social = curState.CurrentSocialState.ToString().Replace('_', ' ');
            socialText.Text = social;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnBoredIncreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentBoredState++;
            App.SaveState();
            bored = curState.CurrentBoredState.ToString().Replace('_', ' ');
            boredText.Text = bored;
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }

        void OnBoredDecreaseClicked(object sender, EventArgs args)
        {
            curState.CurrentBoredState--;
            App.SaveState();
            bored = curState.CurrentBoredState.ToString().Replace('_', ' ');
            boredText.Text = bored;
            MossImage = SpriteCalculator.CalculateAnimationPath();
        }
    }
}