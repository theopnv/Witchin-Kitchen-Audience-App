using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
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
        public abstract void OnCastButtonClick();
    }

}
