using System.Collections.Generic;
using audience.messages;
using UnityEngine;

namespace audience
{

    public static class GameInfo
    {
        public static int PlayerNumber;
        public static int[] PlayerIDs;
        public static Color[] PlayerColors;
        public static string[] PlayerNames;
        public static int[] PlayerPotions;
        public static int[] PlayerIngredients;
        public static bool InGame;
    }

    /// <summary>
    /// Static class to pass player data from lobby scene to game scene
    /// Unfortunately we can't use an array of static PlayerInfo[] because arrays
    /// can't be made of static types. We therefore have to use arrays for each attribute.
    /// </summary>
    public static class ViewerInfo
    {
        public static string SocketId;
        public static string Name;
    }

    public static class TransmitIngredientPoll
    {
        public static IngredientPoll Instance = null;
        public static bool WasAskedToVote = false;
        public static bool Voted = false;
    }
}
