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
        public bool isAsleep;
        public int idleAnimation;
        public int databaseID;
        public string name, ownerName;

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
        public static CurrentState ParseFromString(string input)
        {
            CurrentState result = new CurrentState();
            try
            {
                string[] inputs = input.Split(' ');
                result.CurrentFoodState = (FoodState)Int32.Parse(inputs[0]);
                result.CurrentDrinkState = (DrinkState)Int32.Parse(inputs[1]);
                result.CurrentSocialState = (SocialState)Int32.Parse(inputs[2]);
                result.CurrentEnergyState = (EnergyState)Int32.Parse(inputs[3]);
                result.CurrentBoredState = (BoredState)Int32.Parse(inputs[4]);
                result.isAsleep = Int32.Parse(inputs[5]) == 1;
                result.GenerateNewIdleAnimation();
                return result; 
            }
            catch
            {
                Console.WriteLine("Tried to parse a current_state file that does not have the correct format. Returned default state instead");
                return new CurrentState();
            }
        }

        public string ParseToString()
        {
            string result = string.Empty;

            result += (int)CurrentFoodState; result += " ";
            result += (int)CurrentDrinkState; result += " ";
            result += (int)CurrentSocialState; result += " ";
            result += (int)CurrentEnergyState; result += " ";
            result += (int)CurrentBoredState; result += " ";

            int t = 0;
            if (isAsleep) { t = 1; }
            result += t; result += " ";

            return result;
        }

        public void GenerateNewIdleAnimation()
        {
            Random r = new Random();
            idleAnimation = r.Next(0, 11);
        }
    }
}