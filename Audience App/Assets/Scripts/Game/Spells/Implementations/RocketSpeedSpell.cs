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
            return "Megapunch";
        }

        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.rocket_speed;
        }

        public override string GetTitle()
        {
            return "Rocket Speed";
        }

        public override string GetDescription()
        {
            return "Turn the player into a jetpack with this crazy spell";
        }

    }

}
