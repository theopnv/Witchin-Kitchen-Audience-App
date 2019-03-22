using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{
    public class PrimaryPanelManager : ATutorialPanelManager
    {
        [SerializeField] private GameObject _SpellsManagerPrefab;

        protected override void Start()
        {
            base.Start();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        void OnDisable()
        {
        }

        public void NextPage()
        { 
            Instantiate(_SpellsManagerPrefab, _Canvas.transform).GetComponent<SpellsPanelManager>();
        }
    }

}
