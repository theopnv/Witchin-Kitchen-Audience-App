using System.Collections.Generic;
using UnityEngine;

namespace audience
{

    public static class GameInfo
    {
        public static int[] PlayerIDs;
        public static Color[] PlayerColors;
        public static string[] PlayerNames;
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
