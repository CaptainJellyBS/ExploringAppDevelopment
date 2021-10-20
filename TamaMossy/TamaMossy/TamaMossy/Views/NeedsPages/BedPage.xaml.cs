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
    public partial class BedPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        bool playingAnimation;

        public string ButtonText { get; set; }

        public BedPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetSprite();
            SetButtonText();
        }

        void ClickToggle(object sender, EventArgs e)
        {
            if (playingAnimation) { return; }

            playingAnimation = true;
            Timer t = new Timer { AutoReset = false, Interval = 3000 };
            t.Elapsed += ResetAnim;            

            if (App.CurState.IsAsleep) 
            {
                MossImage = "mossy_sleep_wakeup_large.gif";
                App.CurState.IsAsleep = false;
            }
            else 
            {
                if (App.CurState.CurrentEnergyState >= EnergyState.Rested)
                {
                    MossImage = "mossy_sleep_refuse_large.gif";
                }
                else
                {
                    MossImage = "mossy_sleep_gotosleep_large.gif";
                    App.CurState.IsAsleep = true;
                }
            }            

            t.Start();
        }

        void ResetAnim(object sender, EventArgs e)
        {
            playingAnimation = false;
            SetSprite();
            SetButtonText();
        }

        public void SetSprite()
        {
            if (App.CurState.IsAsleep) { MossImage = "sleep_anim_large.gif"; }
            else { MossImage = "mossy_kitchen_idle_large.gif"; }
        }

        public void SetButtonText()
        {
            if (App.CurState.IsAsleep) { ButtonText = "Wake up"; }
            else { ButtonText = "Go sleep"; }
        }
    }
}