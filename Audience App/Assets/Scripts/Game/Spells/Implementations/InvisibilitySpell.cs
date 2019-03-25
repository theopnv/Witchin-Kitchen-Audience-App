using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class InvisibilitySpell : ASpell
    {
        public override string GetSpritePath()
        {
            return "Spells/InvisibilityCloak";
        }

        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.invisibility;
        }

        public override string GetTitle()
        {
            return "Invisibility";
        }

        public override string GetDescription()
        {
            return "John Cena's specialty. Cast an invisibility spell!";
        }

    }

}
