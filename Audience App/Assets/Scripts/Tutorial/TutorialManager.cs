using System.Collections;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;
        [SerializeField] private GameObject _PollPanelPrefab;
        [SerializeField] private GameObject _GameOutcomePanelPrefab;
        [SerializeField] private GameObject _SpellsPanelPrefab;

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
        }

        void OnDisable()
        {

        }

        #endregion

    }

}
