using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audience.game
{
    public static class Ingredients
    {

        public enum IngredientID
        {
            newt_eye = 0,
            mushroom = 1,
            fish = 2,
            pumpkin = 3,

            max_id,
        }

        public static Dictionary<IngredientID, string> IngredientNames = new Dictionary<IngredientID, string>
        {
            { IngredientID.newt_eye, "Newt Eye"},
            { IngredientID.mushroom, "Mushroom"},
            { IngredientID.fish, "Fish"},
            { IngredientID.pumpkin, "Pumpkin"},
        };


    }

}