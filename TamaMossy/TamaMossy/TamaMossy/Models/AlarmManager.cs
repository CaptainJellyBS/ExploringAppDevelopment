using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TamaMossy.Models
{

    //TODO: Rewrite to not use while loops, but calculate the amount of elapses instead
        //Figuring out how to do that would take too long, so made early stoppers instead to limit the amount of loops
    public class AlarmManager
    {
        public DateTime FoodAlarm { get; set; }
        public DateTime DrinkAlarm { get; set; }
        public DateTime SocialAlarm { get; set; }
        public DateTime EnergyAlarm { get; set; }
        public DateTime BoredAlarm { get; set; }
        Random r = new Random();

        public void TimerInitialize()
        {
            FoodAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5));
            DrinkAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5));
            SocialAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5));
            EnergyAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0));
            BoredAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0));
        }

        public void UpdateTimers()
        {
            
            UpdateFoodAlarm();
            UpdateDrinkAlarm();
            UpdateSocialAlarm();
            UpdateEnergyAlarm();
            UpdateBoredAlarm();
            UpdateFriendsList();

            if (App.CurState.IsAsleep) { App.CurState.IsAsleep = !ShouldIWakeUp(); }
            else { App.CurState.IsAsleep = ShouldIPassOut(); }

            SaveAlarms();
            App.SaveState();
        }

        private void UpdateFoodAlarm()
        {
            if(FoodAlarm == null) { FoodAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); }
            while(FoodAlarm < DateTime.Now)
            {
                if(App.CurState.CurrentFoodState == FoodState.Starving) { FoodAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); break; }
                App.CurState.CurrentFoodState--;

                DependencyService.Get<INotificationManager>().SendNotification
                    (NotificationCalculator.CalculateFoodNotification(App.CurState.CurrentFoodState));
                FoodAlarm = FoodAlarm.AddHours(RandomDouble(2.0, 3.5));
                if (App.CurState.IsAsleep) { FoodAlarm = FoodAlarm.AddHours(RandomDouble(0.5, 2.5)); } //Get thirstier slower when asleep

            }
        }

        private void UpdateDrinkAlarm()
        {
            if (DrinkAlarm == null) { DrinkAlarm = DateTime.Now.AddHours(RandomDouble(1.5, 2.5)); }
            while (DrinkAlarm < DateTime.Now)
            {
                if (App.CurState.CurrentDrinkState == DrinkState.Dehydrated) { DrinkAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); break; }
                App.CurState.CurrentDrinkState--;

                DependencyService.Get<INotificationManager>().SendNotification
                    (NotificationCalculator.CalculateDrinkNotification(App.CurState.CurrentDrinkState));
                DrinkAlarm = DrinkAlarm.AddHours(RandomDouble(2.0, 3.5));
                if (App.CurState.IsAsleep) { DrinkAlarm = DrinkAlarm.AddHours(RandomDouble(1.5, 2.5)); } //Get thirstier slower when asleep
            }
        }

        private void UpdateSocialAlarm()
        {
            if (SocialAlarm == null) { SocialAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); }
            while (SocialAlarm < DateTime.Now)
            {
                if (!App.CurState.IsAsleep) //While asleep, the creature's social state does not change
                {
                    if (App.CurState.IsInPark)
                    {
                        //TODO: Rewrite the timer to take into account the amount of other creatures in the park
                        if(App.CurState.CurrentSocialState == SocialState.Panicking) { SocialAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); return; }
                        App.CurState.CurrentSocialState++;
                    }
                    else
                    {
                        if (App.CurState.CurrentSocialState == SocialState.Forlorn) { SocialAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5)); return; }
                        App.CurState.CurrentSocialState--;
                    }
                    SocialAlarm = SocialAlarm.AddHours(RandomDouble(2.0, 3.5));

                    DependencyService.Get<INotificationManager>().SendNotification
                        (NotificationCalculator.CalculateSocialNotification(App.CurState.CurrentSocialState));
                }
                else 
                { 
                    SocialAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.5));
                }
            }
        }

        private void UpdateEnergyAlarm()
        {
            if (EnergyAlarm == null) { EnergyAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0)); }
            while (EnergyAlarm < DateTime.Now)
            {
                if (App.CurState.IsAsleep)
                {
                    if (App.CurState.CurrentEnergyState == EnergyState.Energized)
                    {
                        EnergyAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0)); return;
                    }

                    App.CurState.CurrentEnergyState+=2;
                }
                else
                {
                    if (App.CurState.CurrentEnergyState == EnergyState.Exhausted)
                    {
                        EnergyAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0)); return;
                    }

                    App.CurState.CurrentEnergyState--;                    
                }
                EnergyAlarm = EnergyAlarm.AddHours(RandomDouble(4.0, 6.0));

                DependencyService.Get<INotificationManager>().SendNotification
                    (NotificationCalculator.CalculateEnergyNotification(App.CurState.CurrentEnergyState));
            }
        }

        private void UpdateBoredAlarm()
        {
            if(BoredAlarm == null) { BoredAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 5.0)); }
            if(BoredAlarm < DateTime.Now)
            {
                //Creature doesn't get bored while asleep or in park
                if (!App.CurState.IsAsleep && !App.CurState.IsInPark) { App.CurState.CurrentBoredState = BoredState.Bored; } 
                BoredAlarm = DateTime.Now.AddHours(RandomDouble(3.0, 5.0));

                DependencyService.Get<INotificationManager>().SendNotification
                    (NotificationCalculator.CalculateBoredNotification(App.CurState.CurrentBoredState));
            }
        }


        //I don't want to be coding this. Why doesn't this exist.
        public double RandomDouble(double min, double max)
        {
           return r.NextDouble() * (max - min) + min;
        }

        public void SaveAlarms()
        {
            Preferences.Set("Alarms", JsonConvert.SerializeObject(this));
        }

        public static AlarmManager LoadAlarms()
        {
            if (!Preferences.ContainsKey("Alarms")){ return new AlarmManager(); }
            return JsonConvert.DeserializeObject<AlarmManager>(Preferences.Get("Alarms", null));
        }

        //Calculate whether the creature should wake up because it is rested or has more important needs.
        bool ShouldIWakeUp()
        {
            float threshold = 0.0f;

            //Set a base depending on how tired we are
            switch (App.CurState.CurrentEnergyState)
            {
                case EnergyState.Rested: threshold += 0.1f; break;
                case EnergyState.Energized: threshold += 0.65f; break;
                case EnergyState.Exhausted: threshold -= 0.35f; break;
                case EnergyState.Tired: threshold -= 0.15f; break;
                default: break; 
            }

            //Modify the threshold depending on hunger and thirst. When hungry or thirsty, have a higher chance of waking up.
            switch (App.CurState.CurrentDrinkState)
            {
                case DrinkState.Thirsty: threshold += 0.025f; break;
                case DrinkState.Dehydrated: threshold += 0.1f; break;
                default: break;
            }

            switch (App.CurState.CurrentFoodState)
            {
                case FoodState.Hungry: threshold += 0.01f; break;
                case FoodState.Very_Hungry: threshold += 0.05f; break;
                case FoodState.Starving: threshold += 0.15f; break;
                default: break;
            }

            return r.NextDouble() <= threshold;
        }

        //Calculate whether the creature should pass out from exhaustion.
        bool ShouldIPassOut()
        {
            float threshold = 0.0f;
            if (App.CurState.IsInPark) { return false; } //Never pass out while in the park.
            
            //Set a base depending on how tired we are
            switch(App.CurState.CurrentEnergyState)
            {
                case EnergyState.Tired: threshold += 0.05f; break;
                case EnergyState.Exhausted: threshold += 0.4f; break;
                default: return false; //Never pass out when not tired
            }

            //Modify the threshold depending on hunger and thirst. When needs are met, chances the creature passes out are higher, otherwise lower
            switch(App.CurState.CurrentDrinkState)
            {
                case DrinkState.Slaked: threshold += 0.1f; break;
                case DrinkState.Thirsty: threshold -= 0.1f; break;
                case DrinkState.Dehydrated: threshold -= 0.25f; break;
                default: break;
            }

            switch(App.CurState.CurrentFoodState)
            {
                case FoodState.Stuffed: threshold += 0.15f; break;
                case FoodState.Full: threshold += 0.1f; break;
                case FoodState.Hungry: threshold -= 0.05f; break;
                case FoodState.Very_Hungry: threshold -= 0.15f; break;
                case FoodState.Starving: threshold -= 0.3f; break;
                default: break;
            }

            return r.NextDouble() <= threshold;
        }

        public void ResetBoredTimer()
        {
            BoredAlarm = DateTime.Now.AddHours(RandomDouble(4.0, 6.0));
            SaveAlarms();
            App.SaveState();
        }

        async void UpdateFriendsList()
        {
            if (!App.CurState.IsInPark) { return; }

            PlaygroundDataStore playground = new PlaygroundDataStore();
            List<PlaygroundEntry> creaturesInPark = await playground.ReadAllItems();

            if(creaturesInPark == null) { return; }

            Dictionary<int, float> friendsList = LoadFriendslist();

            foreach (PlaygroundEntry p in creaturesInPark)
            {
                if(friendsList.ContainsKey(p.Creature.ID))
                {
                    friendsList[p.Creature.ID] += GenerateFriendshipChange(friendsList[p.Creature.ID]/10);
                }
                else
                {
                    friendsList.Add(p.Creature.ID, 0.0f);
                }
            }

            SaveFriendsList(friendsList);
        }

        float GenerateFriendshipChange(float mod)
        {
            float result = (float)r.NextDouble();
            result += Math.Max(-0.5f, Math.Min(mod,0.75f));
            result -= 0.05f; //Base chance of positive change is way higher than chance of negative change

            //Change result based on current state. If needy, the chance of a bad change to the relationship is somewhat higher.
            if(App.CurState.CurrentFoodState == FoodState.Starving) { result -= 0.05f; }
            if(App.CurState.CurrentFoodState == FoodState.Very_Hungry) { result -= 0.01f; }

            if(App.CurState.CurrentDrinkState == DrinkState.Dehydrated) { result -= 0.025f; }

            if(App.CurState.CurrentEnergyState == EnergyState.Tired) { result -= 0.025f; }
            if(App.CurState.CurrentEnergyState == EnergyState.Exhausted) { result -= 0.1f; }

            //The social state has a comparatively huge effect, rather than small changes to the chance
            if(App.CurState.CurrentSocialState == SocialState.Overstimulated) { result -= 0.1f; }
            if(App.CurState.CurrentSocialState == SocialState.Panicking) { result -= 0.25f; }

            result /= 10.0f; //We don't want friendship to increase too fast, since this is called every 15ish minutes.
            return result;
        }

        Dictionary<int, float> LoadFriendslist()
        {
            Dictionary<int, float> friendsList;
            if (!Preferences.ContainsKey("Friends")) { friendsList = new Dictionary<int, float>(); SaveFriendsList(friendsList); }

            else { friendsList = JsonConvert.DeserializeObject<Dictionary<int, float>>(Preferences.Get("Friends", null)); };
            return friendsList;
        }

        void SaveFriendsList(Dictionary<int, float> friendsList)
        {
            Preferences.Set("Friends", JsonConvert.SerializeObject(friendsList));
        }

    }
}
