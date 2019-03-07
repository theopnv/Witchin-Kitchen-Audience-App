using System;
using System.Collections.Generic;

namespace audience.game
{

    public class Spells
    {

        public enum SpellID
        {
            disco_mania = 0,
            mega_mage_punch = 1,
            fireball_for_all = 2,
            rocket_speed = 3,

            max_id,
        }

        public static Dictionary<SpellID, Type> EventList = new Dictionary<SpellID, Type>
        {
            { SpellID.disco_mania, typeof(DiscoManiaSpell) },
            { SpellID.mega_mage_punch, typeof(MegaMageSpell) },
            { SpellID.fireball_for_all, typeof(FireBallForAllSpell) },
            { SpellID.rocket_speed, typeof(RocketSpeedSpell) },
        };

    }

}
