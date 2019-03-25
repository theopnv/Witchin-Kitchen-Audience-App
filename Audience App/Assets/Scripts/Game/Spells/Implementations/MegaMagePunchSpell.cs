using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class MegaMageSpell : ASpell
    {
        public override string GetSpritePath()
        {
            return "Spells/Megapunch";
        }

        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.mega_mage_punch;
        }

        public override string GetTitle()
        {
            return "Giant";
        }

        public override string GetDescription()
        {
            return "Transform the player into a giant.";
        }

    }

}
