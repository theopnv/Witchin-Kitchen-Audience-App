﻿using System.Collections.Generic;

namespace audience.game
{

    public class Spells
    {

        public enum SpellID
        {
            network_ad = 0,

            max_id,
        }

        public static Dictionary<SpellID, string> EventList = new Dictionary<SpellID, string>
        {
            { SpellID.network_ad, "Network Ads" },
        };

    }

}