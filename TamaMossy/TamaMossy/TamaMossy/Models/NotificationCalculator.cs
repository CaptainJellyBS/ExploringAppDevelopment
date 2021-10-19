using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    public class NotificationCalculator
    {
        public static NotificationEventArgs CalculateNotification()
        {
            if (App.CurState.IsAsleep) { return null; }//Do not send notifications when asleep

            if (App.CurState.CurrentFoodState == FoodState.Starving)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is starving!", Message = "Feed poor  " + App.CurState.Name + "!"};
            }
            if (App.CurState.CurrentDrinkState == DrinkState.Dehydrated)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is dehydrated!", Message = "Give " + App.CurState.Name + " some water!" };
            }
            if (App.CurState.CurrentSocialState == SocialState.Forlorn)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is feeling forlorn!", Message = App.CurState.Name + " needs to play with friends!" };
            }
            if (App.CurState.CurrentSocialState == SocialState.Panicking)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is panicking!", Message = App.CurState.Name + " needs some alone time!" };
            }
            if (App.CurState.CurrentEnergyState == EnergyState.Exhausted)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is exhausted!", Message = App.CurState.Name + " needs sleep!" };
            }
            if (App.CurState.CurrentFoodState == FoodState.Very_Hungry)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is very hungry!", Message = App.CurState.Name + " needs food!" };
            }
            if (App.CurState.CurrentDrinkState == DrinkState.Thirsty)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is thirsty!", Message = App.CurState.Name + " needs something to drink!" };
            }
            if (App.CurState.CurrentSocialState == SocialState.Lonely)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is lonely!", Message = App.CurState.Name + " needs to play with friends!" };
            }
            if (App.CurState.CurrentSocialState == SocialState.Overstimulated)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is overstimulated!", Message = App.CurState.Name + " needs some alone time!" };
            }
            if (App.CurState.CurrentEnergyState == EnergyState.Tired)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is Tired!", Message = App.CurState.Name + " needs a nap!" };
            }
            if (App.CurState.CurrentFoodState == FoodState.Hungry)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is hungry!", Message = App.CurState.Name + " needs to eat!" };
            }
            if (App.CurState.CurrentBoredState == BoredState.Bored)
            {
                App.CurState.GenerateNewIdleAnimation();
                return new NotificationEventArgs() { Title = App.CurState.Name + " is bored!", Message = "Play with " + App.CurState.Name + " a bit!" };
            }

            return null; //Do not send notifications if the creature doesn't need anything

        }
    }
}

