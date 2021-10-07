using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    class SpriteCalculator
    {
        /// <summary>
        /// Returns a path to a sprite, based on the current needs of the tamagotchi. 
        /// If the tamagotchi has all it needs fulfilled, a random idle animation plays
        /// A new random idle animation is chosen whenever the tamagotchi has a need, 
        /// so the idle animation switches either on startup, or after switching away from a needs animation
        /// </summary>
        /// <returns></returns>
        public static string CalculateAnimationPath()
        {
            //This feels like YandereDev code and I don't know how to make it better ;_;
            if(App.CurState.isAsleep)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "sleep_anim_large.gif";
            }
            if(App.CurState.CurrentFoodState == FoodState.Starving)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "hunger_anim_starving_large.gif";
            }
            if(App.CurState.CurrentDrinkState == DrinkState.Dehydrated)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "thirsty_anim_dehydrated_large.gif";
            }
            if(App.CurState.CurrentSocialState == SocialState.Forlorn)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "social_anim_forlorn_large.gif";
            }
            if(App.CurState.CurrentSocialState == SocialState.Panicking)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "social_anim_panicking_large.gif";
            }
            if(App.CurState.CurrentEnergyState == EnergyState.Exhausted)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "tired_mossy_exhausted_large.gif";
            }
            if(App.CurState.CurrentFoodState == FoodState.Very_Hungry)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "hunger_anim_veryhungry_large.gif";
            }
            if(App.CurState.CurrentDrinkState == DrinkState.Thirsty)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "thirsty_anim_thirsty_large.gif";
            }
            if(App.CurState.CurrentSocialState == SocialState.Lonely)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "social_anim_lonely_large.gif";
            }
            if(App.CurState.CurrentSocialState == SocialState.Overstimulated)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "social_anim_overstimulated_large.gif";
            }
            if(App.CurState.CurrentEnergyState == EnergyState.Tired)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "tired_mossy_tired_large.gif";
            }
            if(App.CurState.CurrentFoodState == FoodState.Hungry)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "hunger_anim_hungry_large.gif";
            }
            if(App.CurState.CurrentBoredState == BoredState.Bored)
            {
                App.CurState.GenerateNewIdleAnimation();
                return "bored_anim_large.gif";
            }

            return "idle_anim_" + App.CurState.idleAnimation + "_large.gif";

        }
    }
}
