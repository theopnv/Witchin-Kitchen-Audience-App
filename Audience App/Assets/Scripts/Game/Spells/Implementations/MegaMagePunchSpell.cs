using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class MegaMageSpell : ASpell
    {
        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.mega_mage_punch;
        }

        public override string GetTitle()
        {
            return "Mega Mage Punch";
        }

        public override string GetDescription()
        {
            return "Punch is temporarily twice as strong and player grows in size.";
        }

    }

}
