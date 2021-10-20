using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views.NeedsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KitchenPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }
        public string Food { get; set; }
        public string Drink { get; set; }

        Random r = new Random();
        bool playingAnimation = false;
        public KitchenPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this;

            Food = App.CurState.CurrentFoodState.ToString().Replace('_', ' ');
            Drink = App.CurState.CurrentDrinkState.ToString().Replace('_', ' ');

            MossImage = "mossy_kitchen_idle_large.gif";
        }

        void OnFeedClicked(object sender, EventArgs e)
        {
            if (playingAnimation) { return; }
            
            //Refuse food if too full
            if(App.CurState.CurrentFoodState >= FoodState.Full)
            {
                playingAnimation = true;
                if (r.NextDouble() <= 0.1)
                { MossImage = "mossy_kitchen_refuse_throw_large.gif"; } //Small chance for a special refuse animation
                else { MossImage = "mossy_kitchen_refuse_large.gif"; }

                Timer t = new Timer() { Interval = 3000, AutoReset = false };
                t.Elapsed += ResetAnimation;
                t.Start();
            }
            else
            {
                playingAnimation = true;
                MossImage = "mossy_kitchen_eat_large.gif";

                Timer t = new Timer() { Interval = 3000, AutoReset = false };
                t.Elapsed += ResetAnimation;
                App.CurState.CurrentFoodState += 3;
                t.Start();
            }

            App.SaveState();
        }

        void OnDrinkClicked(object sender, EventArgs e)
        {
            if (playingAnimation) { return; }

            //Refuse food if too full
            if (App.CurState.CurrentDrinkState >= DrinkState.Fine)
            {
                playingAnimation = true;
                if(r.NextDouble() <= 0.1)
                { MossImage = "mossy_kitchen_drinkrefuse_squirt_large.gif"; } //Small chance for a special refuse animation
                else { MossImage = "mossy_kitchen_drinkrefuse_large.gif"; }
                

                Timer t = new Timer() { Interval = 3000, AutoReset = false };
                t.Elapsed += ResetAnimation;
                t.Start();
            }
            else
            {
                playingAnimation = true;
                MossImage = "mossy_kitchen_drink_large.gif";

                Timer t = new Timer() { Interval = 3000, AutoReset = false };
                t.Elapsed += ResetAnimation;
                App.CurState.CurrentDrinkState += 2;
                t.Start();
            }

            App.SaveState();
        }

        void ResetAnimation(object sender, EventArgs e)
        {
            MossImage = "mossy_kitchen_idle_large.gif";
            Food = App.CurState.CurrentFoodState.ToString().Replace('_', ' ');
            Drink = App.CurState.CurrentDrinkState.ToString().Replace('_', ' ');

            playingAnimation = false;
        }
    }
}