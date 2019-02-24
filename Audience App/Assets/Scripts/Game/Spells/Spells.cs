using System;
using System.Collections.Generic;

namespace audience.game
{

    public class Spells
    {

        public enum SpellID
        {
            disco_mania = 0,

            max_id,
        }

        public static Dictionary<SpellID, Type> EventList = new Dictionary<SpellID, Type>
        {
            { SpellID.disco_mania, typeof(DiscoManiaSpell) },
        };

    }

}
