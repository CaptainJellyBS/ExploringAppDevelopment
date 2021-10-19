using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

#region State enum declarations
public enum FoodState { Starving, Very_Hungry, Hungry, Peckish, Fine, Full, Stuffed }; //Goes down over time, goes up by feeding
public enum DrinkState { Dehydrated, Thirsty, Could_Drink, Fine, Slaked }; //Goes down over time, goes up by giving drink

//Social state needs to be kept balanced. It goes up while in park (speed depends on amount of other park occupants), goes down while not in park
public enum SocialState { Forlorn, Lonely, Fine, Great, Socially_Satisfied, Overstimulated, Panicking }; 
public enum EnergyState { Exhausted, Tired, Drowsy, Fine, Rested, Energized }; //Goes down while awake, goes up while sleeping
public enum BoredState { Bored, Satisfied }; //Mossy gets bored at random intervals. Minigame possibilities
#endregion

namespace TamaMossy.Models
{
    public class CurrentState
    {
        public bool IsAsleep { get; set; }
        public bool IsInPark { get; set; }
        public int idleAnimation;
        public string Name { get; set; }

        #region ugly state enum getter setter headache
        FoodState currentFoodState;
        DrinkState currentDrinkState;
        SocialState currentSocialState;
        EnergyState currentEnergyState;
        BoredState currentBoredState;

        //Ugly getter setters to make sure the enums don't go out of bounds, which makes things like giving the tamagotchi food easier.
        //Yes, it hurts me too

        public FoodState CurrentFoodState 
        { 
            get { return currentFoodState; } 
            set { currentFoodState = (FoodState)Math.Min(Math.Max((int)value, (int)FoodState.Starving), (int)FoodState.Stuffed); } 
        }

        public DrinkState CurrentDrinkState
        {
            get { return currentDrinkState; }
            set { currentDrinkState = (DrinkState)Math.Min(Math.Max((int)value, (int)DrinkState.Dehydrated), (int)DrinkState.Slaked); }
        }
        
        public SocialState CurrentSocialState
        {
            get { return currentSocialState; }
            set { currentSocialState = (SocialState)Math.Min(Math.Max((int)value, (int)SocialState.Forlorn), (int)SocialState.Panicking); }
        }

        public EnergyState CurrentEnergyState
        {
            get { return currentEnergyState; }
            set { currentEnergyState = (EnergyState)Math.Min(Math.Max((int)value, (int)EnergyState.Exhausted), (int)EnergyState.Energized); }
        }

        public BoredState CurrentBoredState
        {
            get { return currentBoredState; }
            set { currentBoredState = (BoredState)Math.Min(Math.Max((int)value, (int)BoredState.Bored), (int)BoredState.Satisfied); }
        }


        #endregion

        public void GenerateNewIdleAnimation()
        {
            Random r = new Random();
            idleAnimation = r.Next(0, 11);
        }

        public CreatureData ToCreatureData()
        {
            return new CreatureData()
            {
                ID = Preferences.Get("ID", 0),
                UserName = Preferences.Get("PlayerName", "PLACEHOLDER"),
                Name = Name,
                Hunger = Utility.FoodToFloat(CurrentFoodState),
                Thirst = Utility.DrinkToFloat(CurrentDrinkState),
                Stimulated = Utility.SocialToFloat(CurrentSocialState),
                Loneliness = 1 - Utility.SocialToFloat(CurrentSocialState),
                Tired = Utility.EnergyToFloat(CurrentEnergyState),
                Boredom = Utility.BoredToFloat(CurrentBoredState)
            };
        }

        public static CurrentState FromCreatureData(CreatureData data)
        { 
            return new CurrentState()
            {
                Name = data.Name,
                CurrentFoodState = Utility.FoodFromFloat(data.Hunger),
                CurrentDrinkState = Utility.DrinkFromFloat(data.Thirst),
                CurrentSocialState = Utility.SocialFromFloat(data.Stimulated),
                CurrentEnergyState = Utility.EnergyFromFloat(data.Tired),
                CurrentBoredState = Utility.BoredFromFloat(data.Boredom)
            };
        }
    }
}