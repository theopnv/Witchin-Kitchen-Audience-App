using System.Collections.Generic;
using UnityEngine;

namespace audience
{

    public static class GameInfo
    {
        public static int PlayerNumber = 0;
        public static int[] PlayerIDs = new int[4];
        public static Color[] PlayerColors = new Color[4];
        public static string[] PlayerNames = new string[4];
        public static int[] PlayerPotions = new int[4];
        public static int[] PlayerIngredients = new int[4];
        public static bool InGame = false;
    }

    /// <summary>
    /// Static class to pass player data from lobby scene to game scene
    /// Unfortunately we can't use an array of static PlayerInfo[] because arrays
    /// can't be made of static types. We therefore have to use arrays for each attribute.
    /// </summary>
    public static class ViewerInfo
    {
        public static string SocketId = "";
        public static Color Color = Color.blue;
        public static string Name = "";
    }
}
