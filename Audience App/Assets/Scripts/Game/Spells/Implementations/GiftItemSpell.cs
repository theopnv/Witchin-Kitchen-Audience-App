using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class GiftItemSpell : ASpell
    {
        public override string GetSpritePath()
        {
            return "Spells/GiftIngredient";
        }

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
            return "Give a player a gift they're sure to love!";
        }

    }

}
