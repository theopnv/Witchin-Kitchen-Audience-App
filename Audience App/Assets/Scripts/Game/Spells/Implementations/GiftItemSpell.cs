using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class GiftItemSpell : ASpell
    {
        public override Spells.SpellID GetSpellID()
        {
            return Spells.SpellID.gift_item;
        }

        public override string GetTitle()
        {
            return "Gift Item";
        }

        public override string GetDescription()
        {
            return "Gift item desc.";
        }

    }

}
