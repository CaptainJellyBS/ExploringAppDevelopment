using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamaMossy.Models;
using TamaMossy.Views.GamesPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views.NeedsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public GamesPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            MossImage = SpriteCalculator.CalculateAnimationPath();

        }
        void OnTicTacToeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TicTacToePage());

        }
    }
}