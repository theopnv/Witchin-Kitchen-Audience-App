using System.Collections.Generic;
using UnityEngine;

namespace audience
{

    public static class GameInfo
    {
        public static int PlayerNumber = 4;
        public static int[] PlayerIDs = new int[PlayerNumber];
        public static Color[] PlayerColors = new Color[PlayerNumber];
        public static string[] PlayerNames = new string[PlayerNumber];
        public static int[] PlayerPotions = new int[PlayerNumber];
        public static int[] PlayerIngredients = new int[PlayerNumber];
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
