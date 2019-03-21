using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class FireBallForAllSpell : ASpell
    {
        public override string GetSpritePath()
        {
            return "FireballTurret";
        }

        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.fireball_for_all;
        }

        public override string GetTitle()
        {
            return "Fireballs for all";
        }

        public override string GetDescription()
        {
            return "Turn the player into a deadly fireballs turret!";
        }

    }

}
