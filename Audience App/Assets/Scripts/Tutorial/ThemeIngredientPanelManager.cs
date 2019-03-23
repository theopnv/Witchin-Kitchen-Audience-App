using UnityEngine;

namespace audience.tutorial
{
    public class ThemeIngredientPanelManager : ATutorialPanelManager
    {
        [SerializeField] private GameObject _EventPanel;
        [SerializeField] private GameObject _EndPanel;

        #region Custom Methods


        public void NextPage()
        {
            Instantiate(_EndPanel, _Canvas.transform).GetComponent<EndPanelManager>();
        }

        public void PreviousPage()
        {
            Instantiate(_EventPanel, _Canvas.transform).GetComponent<EventPanelManager>();
            Destroy(gameObject);
        }

        #endregion
    }
}