using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    public static class Utility
    {
        public static float FoodToFloat(FoodState f)
        {
            switch(f)
            {
                case FoodState.Starving: return 0.0f;
                case FoodState.Very_Hungry: return 0.15f;
                case FoodState.Hungry: return 0.3f;
                case FoodState.Peckish: return 0.5f;
                case FoodState.Fine: return 0.6f;
                case FoodState.Full: return 0.8f;
                case FoodState.Stuffed: return 1.0f;
                default: throw new ArgumentException();
            }
        }

        public static FoodState FoodFromFloat(float f)
        {
            //Can't use switch cases because using older version of C#. Might fix.
            if(f < 0.15f) { return FoodState.Starving; }
            if(f<0.3f) { return FoodState.Very_Hungry; }
            if(f < 0.5f) { return FoodState.Hungry; }
            if(f<0.6f) { return FoodState.Peckish; }
            if(f < 0.8f) { return FoodState.Fine; }
            if(f< 1.0f) { return FoodState.Full; }
            return FoodState.Stuffed;
        }

        public static float DrinkToFloat(DrinkState d)
        {
            switch (d)
            {
                case DrinkState.Dehydrated: return 0.0f;
                case DrinkState.Thirsty: return 0.2f;
                case DrinkState.Could_Drink: return 0.4f;
                case DrinkState.Fine: return 0.6f;
                case DrinkState.Slaked: return 0.8f;
                default: throw new ArgumentException();
            }
        }

        public static DrinkState DrinkFromFloat(float d)
        {
            //Can't use switch cases because using older version of C#. Might 
            if(d < 0.2f) { return DrinkState.Dehydrated; }
            if(d < 0.4f) { return DrinkState.Thirsty; }
            if(d < 0.6f) { return DrinkState.Could_Drink; }
            if(d < 0.8f) { return DrinkState.Fine; }
            return DrinkState.Slaked;
        }

        public static float SocialToFloat(SocialState s)
        {
            switch(s)
            {
                case SocialState.Forlorn: return 0.0f;
                case SocialState.Lonely: return 0.1f;
                case SocialState.Fine: return 0.3f;
                case SocialState.Great: return 0.5f;
                case SocialState.Socially_Satisfied: return 0.7f;
                case SocialState.Overstimulated: return 0.9f;
                case SocialState.Panicking: return 1.0f;
                default: throw new ArgumentException();
            }
        }

        public static SocialState SocialFromFloat(float s)
        {
            if(s < 0.1f) { return SocialState.Forlorn; }
            if(s < 0.3f) { return SocialState.Lonely; }
            if(s < 0.5f) { return SocialState.Fine; }
            if(s < 0.7f) { return SocialState.Great; }
            if(s < 0.9f) { return SocialState.Socially_Satisfied; }
            if(s < 1.0f) { return SocialState.Overstimulated; }
            return SocialState.Panicking;
        }

        public static float EnergyToFloat(EnergyState e)
        {
            switch(e)
            {
                case EnergyState.Exhausted: return 0.0f;
                case EnergyState.Tired: return 0.2f;
                case EnergyState.Drowsy: return 0.4f;
                case EnergyState.Fine: return 0.6f;
                case EnergyState.Rested: return 0.8f;
                case EnergyState.Energized: return 1.0f;
                default: throw new ArgumentException();
            }
        }

        public static EnergyState EnergyFromFloat(float e)
        {
            if(e < 0.2f) { return EnergyState.Exhausted; }
            if(e < 0.4f) { return EnergyState.Tired; }
            if(e<0.6f) { return EnergyState.Drowsy; }
            if(e < 0.8f) { return EnergyState.Fine; }
            if(e < 1.0f) { return EnergyState.Rested; }
            return EnergyState.Energized;
        }

        public static float BoredToFloat(BoredState b)
        {
            if(b == BoredState.Satisfied) { return 1.0f; }
            return 0.0f;
        }

        public static BoredState BoredFromFloat(float b)
        {
            if(b < 1.0f) { return BoredState.Bored; }
            return BoredState.Satisfied;
        }

    }
}
