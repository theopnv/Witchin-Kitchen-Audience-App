using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{

    public class GameManager : MonoBehaviour
    {
        private NetworkManager _NetworkManager;

        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;

        [SerializeField] private GameObject _PollPanelPrefab;
        [HideInInspector] public PollPanelManager PollPanelManager;

        [SerializeField] private GameObject _ExitRoomPanelPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
        }

        public void ExitRoom()
        {
            _NetworkManager.ExitRoom();
            Destroy(_NetworkManager.gameObject);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        public void StartPoll(PollChoices pollChoices)
        {
            var pollPanel = Instantiate(_PollPanelPrefab, _Canvas.transform);
            PollPanelManager = pollPanel.GetComponent<PollPanelManager>();
            PollPanelManager.StartPoll(pollChoices, _NetworkManager);
        }

        public void SetGameOutcome(GameOutcome gameOutcome)
        {
            var exitRoomPanel = Instantiate(_ExitRoomPanelPrefab, _Canvas.transform);
            var exitRoomPanelManager = exitRoomPanel.GetComponent<ExitRoomPanelManager>();
            exitRoomPanelManager.SetOutcome(gameOutcome);
        }

    }

}
