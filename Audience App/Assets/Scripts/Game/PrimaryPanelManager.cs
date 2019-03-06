using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{
    public class PrimaryPanelManager : MonoBehaviour
    {
        [HideInInspector] public GameManager GameManager;

        public void BrowseSpells()
        {
            GameManager.StartSpellSelection();
        }

        public void ExitRoom()
        {
            GameManager.ExitRoom();
        }
    }

}
