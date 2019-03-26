using System.Collections;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;

        [SerializeField] private GameObject _OneChoiceOverlayPrefab;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;

        private NetworkManager _NetworkManager;

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnReceivedStopIngredientPoll += OnReceivedStopIngredientPoll;
            _NetworkManager.OnDisconnected += OnDisconnectedFromServer;
        }

        void OnDisable()
        {
            _NetworkManager.OnReceivedStopIngredientPoll -= OnReceivedStopIngredientPoll;
            _NetworkManager.OnDisconnected -= OnDisconnectedFromServer;
        }

        void OnDisconnectedFromServer()
        {
            var instance = Instantiate(_OneChoiceOverlayPrefab, _Canvas.transform);
            var errorOverlay = instance.GetComponent<Overlay>();
            errorOverlay.Description = "Lost the magic link (Server disconnection). Try to join again.";
            errorOverlay.Primary += Exit;
        }

        void Exit()
        {
            GameInfo.InGame = false;
            _NetworkManager?.ExitRoom();
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        void OnReceivedStopIngredientPoll()
        {
            TransmitIngredientPoll.WasAskedToVote = false;
        }

        #endregion

    }

}
