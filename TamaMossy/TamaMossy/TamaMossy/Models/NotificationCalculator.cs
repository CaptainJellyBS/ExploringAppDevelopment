using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    public class NotificationCalculator
    {
        public static NotificationEventArgs CalculateFoodNotification(FoodState f)
        {
            if (App.CurState.IsAsleep) { return null; }
            switch(f)
            {
                case FoodState.Stuffed:
                case FoodState.Full:
                case FoodState.Fine:
                case FoodState.Peckish: return null;
                case FoodState.Hungry: 
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is hungry!", Message = App.CurState.Name + " needs to eat!" };
                case FoodState.Very_Hungry: 
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is very hungry!", Message = App.CurState.Name + " needs food!" };
                case FoodState.Starving: 
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is starving!", Message = "Feed poor  " + App.CurState.Name + "!" };
                default: return null;           
            }
        }

        public static NotificationEventArgs CalculateDrinkNotification(DrinkState d)
        {
            if (App.CurState.IsAsleep) { return null; }
            switch (d)
            {
                case DrinkState.Slaked:
                case DrinkState.Fine:
                case DrinkState.Could_Drink: return null;
                case DrinkState.Thirsty: 
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is thirsty!", Message = App.CurState.Name + " needs something to drink!" };
                case DrinkState.Dehydrated:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is dehydrated!", Message = "Give " + App.CurState.Name + " something to drink!" };
                default: return null;
            }
        }

        public static NotificationEventArgs CalculateSocialNotification(SocialState s)
        {
            if (App.CurState.IsAsleep) { return null; }
            switch (s)
            {
                case SocialState.Socially_Satisfied:
                case SocialState.Fine:
                case SocialState.Great: return null;
                case SocialState.Lonely:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is lonely!", Message = App.CurState.Name + " needs to play with friends!" };
                case SocialState.Overstimulated:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is overstimulated!", Message = App.CurState.Name + " needs some alone time!" };
                case SocialState.Forlorn:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is feeling forlorn!", Message = App.CurState.Name + " needs to play with friends!" };
                case SocialState.Panicking:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is panicking!", Message = App.CurState.Name + " needs some alone time!" };
                default: return null;
            }
        }

        public static NotificationEventArgs CalculateEnergyNotification(EnergyState e)
        {
            if (App.CurState.IsAsleep) { return null; }
            switch (e)
            {
                case EnergyState.Exhausted:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is exhausted!", Message = App.CurState.Name + " needs sleep!" };
                case EnergyState.Tired:
                    return new NotificationEventArgs() { Title = App.CurState.Name + " is tired!", Message = App.CurState.Name + " needs a nap!" };
                case EnergyState.Drowsy: 
                case EnergyState.Fine: 
                case EnergyState.Rested:
                case EnergyState.Energized: 
                default: return null;
            }
        }

        public static NotificationEventArgs CalculateBoredNotification(BoredState b)
        {
            if (App.CurState.IsAsleep) { return null; }
            if (b == BoredState.Satisfied) { return null; }
            return new NotificationEventArgs() { Title = App.CurState.Name + " is bored!", Message = "Play with " + App.CurState.Name + " a bit!" };
        }
    }
}

