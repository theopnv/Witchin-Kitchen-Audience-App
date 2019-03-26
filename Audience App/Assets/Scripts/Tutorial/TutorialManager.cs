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
        [SerializeField] private GameObject _PollPanelPrefab;
        [SerializeField] private GameObject _SpellsPanelPrefab;

        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;

        private NetworkManager _NetworkManager;

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnReceivedStopIngredientPoll += OnReceivedStopIngredientPoll;
        }

        void OnDisable()
        {
            _NetworkManager.OnReceivedStopIngredientPoll -= OnReceivedStopIngredientPoll;
        }

        void OnReceivedStopIngredientPoll()
        {
            TransmitIngredientPoll.WasAskedToVote = false;
        }

        #endregion

    }

}
