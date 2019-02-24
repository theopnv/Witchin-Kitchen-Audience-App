using System.Collections;
using System.Collections.Generic;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public class DiscoManiaSpell : ASpell
    {
        public override string GetTitle()
        {
            return "Disco Mania";
        }

        public override string GetDescription()
        {
            return "Turn the arena into a giant dancefloor!";
        }

        public override void OnCastButtonClick()
        {
            var spell = new Spell
            {
                spellId = (int)Spells.SpellID.disco_mania,
                targetedPlayer = new Player() { id = 1, }
            };
            _NetworkManager.EmitSpellCast(spell);
        }
    }

}
