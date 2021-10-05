using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string mossImage;
        public string MossImage { get { return mossImage; } set { mossImage = value; OnPropertyChanged("MossImage"); } }

        public MainPage()
        {
            BindingContext = this;

            Random r = new Random();
            MossImage = Path.Combine("idle_anim_", r.Next(0, 11).ToString(), "_large.gif");
            Console.WriteLine(mossImage);
            //mossImage = new Image { Source = ("idle_anim_" + r.Next(0, 11) + "_Large.gif"), IsAnimationPlaying=true};

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = this;

            Random r = new Random();
            MossImage = "idle_anim_" + r.Next(0, 11) + "_large.gif";
            Console.WriteLine(mossImage);
        }
    }
}