using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamaMossy.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NamesPage : ContentPage
    { 
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public NamesPage()
        {           
            BindingContext = this;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MossImage = SpriteCalculator.CalculateAnimationPath();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //Because the text completed event is not fired when closing the keyboard with the back button (why Xamarin? WHY?), do this
            Preferences.Set("PlayerName", UsernameEntry.Text);
            App.CurState.Name = NameEntry.Text;
        }

        void OnUsernameCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
            Preferences.Set("PlayerName", text);
        }

        void OnNameCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
            App.CurState.Name = text;
        }

        void OnDoneClicked(object sender, EventArgs e)
        {
            CurrentState test = App.CurState;
            App.SaveState();
            Navigation.PopAsync();
        }
    }
}