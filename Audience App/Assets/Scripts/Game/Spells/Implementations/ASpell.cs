using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
using audience.messages;
using UnityEngine;

namespace audience.game
{

    public abstract class ASpell : MonoBehaviour, ISpell
    {
        protected NetworkManager _NetworkManager;

        public void SetNetworkManager(NetworkManager nm)
        {
            _NetworkManager = nm;
        }

        public abstract string GetTitle();
        public abstract string GetDescription();
        public abstract string GetSpritePath();
        public abstract Spells.SpellID GetSpellID();

        public void OnCastButtonClick(int targetId)
        {
            var spell = new Spell
            {
                spellId = (int)GetSpellID(),
                targetedPlayer = new Player
                {
                    id = targetId,
                },
                caster = new Viewer
                {
                    color = ColorUtility.ToHtmlStringRGBA(ViewerInfo.Color),
                    name = ViewerInfo.Name,
                    socketId = ViewerInfo.SocketId
                }
            };
            Debug.Log("Target: " + targetId);
            _NetworkManager.EmitSpellCast(spell);
        }
    }

}
