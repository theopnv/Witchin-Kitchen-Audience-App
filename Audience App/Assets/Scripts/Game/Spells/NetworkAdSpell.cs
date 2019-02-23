using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class NetworkAdSpell : ASpell
    {
        public override string GetTitle()
        {
            return "Network Ads";
        }

        public override string GetDescription()
        {
            return "This is a very small description";
        }

        public override void OnCastButtonClick()
        {
            var spell = new Spell
            {
                spellId = (int)Spells.SpellID.network_ad,
                targetedPlayer = new Player() { id = 1, }
            };
            _NetworkManager.EmitSpellCast(spell);
        }
    }

}
