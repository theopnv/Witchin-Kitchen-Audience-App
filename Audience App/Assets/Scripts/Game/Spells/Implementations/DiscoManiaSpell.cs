using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class DiscoManiaSpell : ASpell
    {
        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.disco_mania;
        }

        public override string GetTitle()
        {
            return "Disco Mania";
        }

        public override string GetDescription()
        {
            return "Turn the arena into a giant dancefloor!";
        }

    }

}
