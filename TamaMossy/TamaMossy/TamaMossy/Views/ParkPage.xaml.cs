using Newtonsoft.Json;
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
        List<PlaygroundEntry> creaturesInPark;
        Dictionary<int, float> friendsList;
        int currentIndex = 0;
        int currentShown = -1;
        List<Button> currentButtons;

        public string ButtonText { get; set; }

        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public ParkPage()
        {
            BindingContext = this;
            remoteStore = new PlaygroundDataStore();
            currentButtons = new List<Button>();

            LoadFriendslist();

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            CalculateButtonText();

            creaturesInPark = await remoteStore.ReadAllItems();
            
            if (creaturesInPark == null)
            {
                await DisplayAlert("Error on loading park", "The app could not connect to the database.", "OK");
                Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            }

            UpdateButtons();
            MossImage = SpriteCalculator.CalculateAnimationPath();
        }

        void OnParkButtonClicked(object sender, EventArgs e)
        {
            if (App.CurState.IsInPark) { RetrieveFromPark(); return; }
            SendToPark();
        }

        #region Park Button
        async void SendToPark()
        {
            if (App.CurState.IsAsleep) { return; }
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

        void CalculateButtonText()
        {
            if (App.CurState.IsAsleep) { ButtonText = App.CurState.Name + " is asleep"; return; }
            if (App.CurState.IsInPark) { ButtonText = "Retrieve " + App.CurState.Name + " from park"; return; }
            ButtonText = "Send " + App.CurState.Name + " to park";
        }

        #endregion

        #region Names grid

        void OnButtonUpClicked(object sender, EventArgs e)
        {
            currentIndex -= 10;

            UpdateButtons();
        }

        void OnButtonDownClicked(object sender, EventArgs e)
        {
            currentIndex += 10;

            UpdateButtons();
        }

        void UpdateButtons()
        {
            UpButton.IsEnabled = currentIndex > 0;
            DownButton.IsEnabled = currentIndex + 10 < creaturesInPark.Count;

            foreach (Button b in currentButtons)
            {
                PageGrid.Children.Remove(b);
            }

            currentButtons = new List<Button>();

            for (int i = currentIndex; i < Math.Min(currentIndex + 10, creaturesInPark.Count); i++)
            {
                //if(creaturesInPark[i].Creature.ID == Preferences.Get("ID", 0)) { continue; } //Don't add a button for ourselves
                
                Button b = new Button
                {
                    Text = creaturesInPark[i].Creature.Name
                };
                
                int k = i; //For some reason it decides to pass ints as reference, so we do this. Thanks I hate it.

                b.Clicked += async (sender, args) => OnCreatureButtonClicked(k); //Wacky lambda shit to make index right
                PageGrid.Children.Add(b, i % 2, 7 + ((i - currentIndex) / 2));
            }


            InfoBox.IsVisible = currentShown >= 0;
            CurrentSelectedCreatureName.IsVisible = currentShown >= 0;
            CurrentSelectedCreatureUserName.IsVisible = currentShown >= 0;
            CurrentSelectedCreatureFriendship.IsVisible = currentShown >= 0;
            OwnerText.IsVisible = currentShown >= 0;
            RelationshipText.IsVisible = currentShown >= 0;

            if(currentShown >= 0)
            {
                if (friendsList.ContainsKey(creaturesInPark[currentShown].Creature.ID))
                {
                    CurrentSelectedCreatureName.Text = creaturesInPark[currentShown].Creature.Name;
                    CurrentSelectedCreatureUserName.Text = creaturesInPark[currentShown].Creature.UserName;
                    if (creaturesInPark[currentShown].Creature.ID == Preferences.Get("ID", 0))
                    {
                        CurrentSelectedCreatureFriendship.Text = "This is me!";
                    }
                    else
                    {
                        CurrentSelectedCreatureFriendship.Text = FriendshipToText(friendsList[creaturesInPark[currentShown].Creature.ID]);
                    }
                }
                else
                {
                    CurrentSelectedCreatureName.Text = "?????";
                    CurrentSelectedCreatureUserName.Text = "?????";
                    CurrentSelectedCreatureFriendship.Text = "?????";
                }
            }
        }

        async Task OnCreatureButtonClicked(int i)
        {
            if (currentShown == i)
            { currentShown = -1; } //Magic numbers \o/
            else
            {
                currentShown = i;
            }

            UpdateButtons();
        }
        #endregion

        PlaygroundEntry CreatePlaygroundEntry()
        {
            return new PlaygroundEntry() { ID = Preferences.Get("ID", 0), EnterTime = DateTime.Now, Creature = App.CurState.ToCreatureData() };
        }
        void LoadFriendslist()
        {
            if (!Preferences.ContainsKey("Friends")) { friendsList = new Dictionary<int, float>(); SaveFriendsList(); return; }

            friendsList = JsonConvert.DeserializeObject<Dictionary<int, float>>(Preferences.Get("Friends", null));
        }

        void SaveFriendsList()
        {
            Preferences.Set("Friends", JsonConvert.SerializeObject(friendsList));
        }

        string FriendshipToText(float f)
        {
            //Haha imagine having the newest version of C#
            if(f >= 10.0f) { return "Besties"; }
            if(f >= 5.0f) { return "Great Friends"; }
            if(f >= 3.0f) { return "Friends"; }
            if(f >= 1.0f) { return "Acquaintances"; }            
            if(f >= 0.0f) { return "Just met"; }

            if (f >= -1.0f) { return "Hostile"; }
            if (f >= -5.0f) { return "Enemies"; }
            return "Nemeses"; 

        }
    }
}