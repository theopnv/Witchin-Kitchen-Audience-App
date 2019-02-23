using System;
using System.Collections.Generic;

namespace audience.game
{

    public class Spells
    {

        public enum SpellID
        {
            network_ad = 0,

            max_id,
        }

        public static Dictionary<SpellID, Type> EventList = new Dictionary<SpellID, Type>
        {
            { SpellID.network_ad, typeof(NetworkAdSpell) },
        };

    }

}
