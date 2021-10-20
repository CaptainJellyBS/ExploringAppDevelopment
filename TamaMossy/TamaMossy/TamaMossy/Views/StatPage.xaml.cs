using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamaMossy.Models;
using TamaMossy.Views.NeedsPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public CurrentState curState;
        public string food { get; set; }
        public string drink { get; set; }
        public string energy { get; set; }
        public string social { get; set; }
        public string bored { get; set; }

        public string kitchenButtonText { get; set; }
        public string gamesButtonText { get; set; }

        public StatPage()
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

            UpdateButtons();
        }

        void OnKitchenClicked(object sender, EventArgs e)
        {
            UpdateButtons();
            if (!curState.IsAsleep) { Navigation.PushAsync(new KitchenPage()); }
        }

        void OnGamesClicked(object sender, EventArgs e)
        {
            UpdateButtons();
            if (!curState.IsAsleep) { Navigation.PushAsync(new GamesPage()); }            
        }

        void OnParkClicked(object sender, EventArgs e)
        {
            (sender as Button).Text = "Parks have not been implemented yet";
        }

        void OnBedClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BedPage());

        }

        void UpdateButtons()
        {
            if (curState.IsAsleep)
            {
                kitchenButtonText = curState.Name + " is asleep";
                gamesButtonText = curState.Name + " is asleep";
            }
            else
            {
                kitchenButtonText = "To Kitchen";
                gamesButtonText = "To Games";
            }
        }
    }
}