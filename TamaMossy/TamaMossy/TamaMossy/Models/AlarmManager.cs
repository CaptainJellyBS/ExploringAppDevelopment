using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TamaMossy.Models
{

    //TODO: Rewrite to not use while loops, but calculate the amount of elapses instead
    public class AlarmManager
    {
        public DateTime FoodAlarm { get; set; }
        public DateTime DrinkAlarm { get; set; }
        public DateTime SocialAlarm { get; set; }
        public DateTime EnergyAlarm { get; set; }
        public DateTime BoredAlarm { get; set; }
        Random r = new Random();

        public AlarmManager()
        {
            //Code that loads timers from json file here
        }

        public void UpdateTimers()
        {
            //Should probably ensure that these are loaded in first
            UpdateFoodAlarm();
            UpdateDrinkAlarm();
            UpdateSocialAlarm();
            UpdateEnergyAlarm();
            UpdateBoredAlarm();
        }

        public void UpdateFoodAlarm()
        {
            if(FoodAlarm == null) { FoodAlarm = DateTime.Now.AddHours(RandomDouble(1.5, 2.5)); }
            while(FoodAlarm < DateTime.Now)
            {
                App.CurState.CurrentFoodState--;
                FoodAlarm = FoodAlarm.AddHours(RandomDouble(1.5,2.5));
            }
        }

        public void UpdateDrinkAlarm()
        {
            if (DrinkAlarm == null) { DrinkAlarm = DateTime.Now.AddHours(RandomDouble(1.5, 2.5)); }
            while (DrinkAlarm < DateTime.Now)
            {
                App.CurState.CurrentDrinkState--;
                DrinkAlarm = DrinkAlarm.AddHours(RandomDouble(1.5, 2.5));
            }
        }

        public void UpdateSocialAlarm()
        {
            if (SocialAlarm == null) { SocialAlarm = DateTime.Now.AddHours(RandomDouble(1.5, 2.5)); }
            while (SocialAlarm < DateTime.Now)
            {
                if (!App.CurState.isAsleep) //While asleep, the creature's social state does not change
                {
                    if (App.CurState.isInPark)
                    {
                        //TODO: Rewrite the timer to take into account the amount of other creatures in the park
                        App.CurState.CurrentSocialState++;
                    }
                    else
                    {
                        App.CurState.CurrentSocialState--;
                    }
                }
                SocialAlarm = SocialAlarm.AddHours(RandomDouble(1.5, 2.5));
            }
        }

        public void UpdateEnergyAlarm()
        {
            if (EnergyAlarm == null) { EnergyAlarm = DateTime.Now.AddHours(RandomDouble(2.0, 3.0)); }
            while (EnergyAlarm < DateTime.Now)
            {
                if (App.CurState.isAsleep)
                {
                    App.CurState.CurrentEnergyState+=2;
                }
                else
                {
                    App.CurState.CurrentEnergyState--;
                }
                EnergyAlarm = EnergyAlarm.AddHours(RandomDouble(2.0, 3.0));
            }
        }

        public void UpdateBoredAlarm()
        {
            if(BoredAlarm == null) { BoredAlarm = DateTime.Now.AddHours(RandomDouble(3.0, 5.0)); }
            if(BoredAlarm < DateTime.Now)
            {
                if (!App.CurState.isAsleep) { App.CurState.CurrentBoredState = BoredState.Bored; } //Creature doesn't get bored while asleep
                BoredAlarm = DateTime.Now.AddHours(RandomDouble(3.0, 5.0));
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
            return (AlarmManager)JsonConvert.DeserializeObject(Preferences.Get("Alarms", null));
        }

    }
}
