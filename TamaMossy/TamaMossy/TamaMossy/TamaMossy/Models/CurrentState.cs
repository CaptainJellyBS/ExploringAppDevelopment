using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

public enum FoodState { Starving, Very_Hungry, Hungry, Peckish, Fine, Full, Stuffed };
public enum DrinkState { Dehydrated, Very_Thirsty, Thirsty, Fine, Slaked };

//Social state needs to be kept balanced
public enum SocialState { Forlorn, Very_Lonely, Lonely, Fine, Great, Overstimulated, Panicking };
public enum EnergyState { Exhausted, Tired, Drowsy, Fine, Rested, Energized };
public enum BoredState { Bored, Satisfied }; //Mossy gets bored at random intervals. Minigame possibilities.

namespace TamaMossy.Models
{
    public class CurrentState
    {
        public FoodState currentFoodState;
        public DrinkState currentDrinkState;
        public SocialState currentSocialState;
        public EnergyState currentEnergyState;
        public BoredState currentBoredState;

        public static CurrentState ParseFromString(string input)
        {
            CurrentState result = new CurrentState();
            try
            {
                string[] inputs = input.Split(' ');
                result.currentFoodState = (FoodState)Int32.Parse(inputs[0]);
                result.currentDrinkState = (DrinkState)Int32.Parse(inputs[1]);
                result.currentSocialState = (SocialState)Int32.Parse(inputs[2]);
                result.currentEnergyState = (EnergyState)Int32.Parse(inputs[3]);
                result.currentBoredState = (BoredState)Int32.Parse(inputs[4]);
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
            result += (int)currentFoodState; result += " ";
            result += (int)currentDrinkState; result += " ";
            result += (int)currentSocialState; result += " ";
            result += (int)currentEnergyState; result += " ";
            result += (int)currentBoredState;

            return result;
        }
    }
}