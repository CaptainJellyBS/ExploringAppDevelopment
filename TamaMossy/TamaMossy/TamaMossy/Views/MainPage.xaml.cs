using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TamaMossy.Models;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = this;
            MossImage = SpriteCalculator.CalculateAnimationPath();
        }
    }
}