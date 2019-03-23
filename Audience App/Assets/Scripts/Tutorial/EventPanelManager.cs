using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{
    public class EventPanelManager : ATutorialPanelManager
    {
        [SerializeField] private GameObject _SpellsPanel;
        [SerializeField] private GameObject _EndPanel;

        #region Custom Methods

        public void NextPage()
        {
            Instantiate(_EndPanel, _Canvas.transform).GetComponent<EndPanelManager>();
        }

        public void PreviousPage()
        {
            Instantiate(_SpellsPanel, _Canvas.transform).GetComponent<SpellsPanelManager>();
            Destroy(gameObject);
        }
        #endregion
    }
}