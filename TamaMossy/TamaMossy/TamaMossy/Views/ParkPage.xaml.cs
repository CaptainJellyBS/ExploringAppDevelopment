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
    public partial class ParkPage : ContentPage
    {
        PlaygroundDataStore remoteStore;
        public string ButtonText { get; set; }
        public ParkPage()
        {
            BindingContext = this;
            remoteStore = new PlaygroundDataStore();
            InitializeComponent();

            CalculateButtonText();

        }

        void OnParkButtonClicked(object sender, EventArgs e)
        {
            if (App.CurState.IsInPark) { RetrieveFromPark(); return; }
            SendToPark();
        }

        async void SendToPark()
        {
            if(await remoteStore.CreateItem(CreatePlaygroundEntry()))
            {
                App.CurState.IsInPark = true;
                App.SaveState();
                CalculateButtonText();
                return;
            }
            throw new Exception("Something went wrong while trying to send creature to park");
        }

        async void RetrieveFromPark()
        {

            if (await remoteStore.DeleteItem(CreatePlaygroundEntry()))
            {
                App.CurState.IsInPark = false;
                App.SaveState();
                CalculateButtonText();
                return;
            }
            throw new Exception("Something went wrong while trying to retrieve creature from park");
        }

        PlaygroundEntry CreatePlaygroundEntry()
        {
            return new PlaygroundEntry() { ID = Preferences.Get("ID", 0), EnterTime = DateTime.Now, Creature = App.CurState.ToCreatureData() };
        }

        void CalculateButtonText()
        {
            if (App.CurState.IsInPark) { ButtonText = "Retrieve " + App.CurState.Name + " from park"; return; }
            ButtonText = "Send " + App.CurState.Name + " to park";
        }
    }
}