using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{
    public class PrimaryPanelManager : APanelManager
    {
        [HideInInspector] public GameManager GameManager;

        private NetworkManager _NetworkManager;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private GameObject _SpellsManagerPrefab;
        [SerializeField] private Canvas _Canvas;

        void Start()
        {
            _Canvas = FindObjectOfType<Canvas>();
            _NetworkManager = FindObjectOfType<NetworkManager>();
        }

        public override void ExitScreen()
        {
            var instance = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform);
            var manager = instance.GetComponent<Overlay>();
            manager.Primary += ExitRoom;
            manager.Secondary += () => { Destroy(manager.gameObject); };
            manager.Description = StringLitterals.RETURN_TO_TITLE_SCREEN_CONFIRMATION;
        }

        public void BrowseSpells()
        {
            var spellManager = Instantiate(_SpellsManagerPrefab, _Canvas.transform).GetComponent<SpellsPanelManager>();
            spellManager.AuthorizeCasting = false;
        }

        public void ExitRoom()
        {
            _NetworkManager?.ExitRoom();
            Destroy(_NetworkManager?.gameObject);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }
    }

}
