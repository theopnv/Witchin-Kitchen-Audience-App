using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audience.game
{
    public static class Ingredients
    {

        public enum IngredientID
        {
            pepper = 0,
            newt_eye = 1,
            mushroom = 2,
            fish = 3,
            pumpkin = 4,

            max_id,
        }

        public static Dictionary<IngredientID, string> IngredientNames = new Dictionary<IngredientID, string>
        {
            { IngredientID.pepper, "Pepper"},
            { IngredientID.newt_eye, "Newt Eye"},
            { IngredientID.mushroom, "Mushroom"},
            { IngredientID.fish, "Fish"},
            { IngredientID.pumpkin, "Pumpkin"},
        };


    }

}