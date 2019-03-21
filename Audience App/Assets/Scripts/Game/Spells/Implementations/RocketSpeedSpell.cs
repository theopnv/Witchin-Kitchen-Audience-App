using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class RocketSpeedSpell : ASpell
    {
        public override string GetSpritePath()
        {
            return "RocketSpeed";
        }

        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.rocket_speed;
        }

        public override string GetTitle()
        {
            return "Rainbow Speed";
        }

        public override string GetDescription()
        {
            return "Make the player move faster with this crazy spell!";
        }

    }

}
