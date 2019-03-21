using System;
using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
using audience.messages;
using UnityEngine;
using UnityEngine.UI;

namespace audience.tutorial
{
    public class EventPanelManager : APanelManager
    {
        private Canvas _Canvas;

        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        // Selection
        [Header("Selection")]
        [SerializeField] private GameObject _SelectionPanel;

        [SerializeField] private GameObject _SpellsPanel;

        // Common
        private Text _RemainingText;
        private int _RemainingTime = 20;

        void Start()
        {
            _Canvas = FindObjectOfType<Canvas>();
            foreach (var spell in Spells.EventList)
            {
                GenerateCard(spell.Value);
            }
        }

        void OnDisable()
        {
        }

        void Update()
        {
        }

        #region Custom Methods

        public override void ExitScreen()
        {
            base.ExitScreen();
        }

        public void OnExitButtonClick()
        {
            var overlay = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform).GetComponent<Overlay>();
            overlay.Primary = () => ExitScreen();
            overlay.Secondary = () => Destroy(overlay.gameObject);
            overlay.Description = "Do you really want to stop watching Witchin Kitchen ?";
        }

        public void NextPage()
        {
            Instantiate(_SelectionPanel, _Canvas.transform).GetComponent<ThemeIngredientPanelManager>();
        }

        public void PreviousPage()
        {
            Instantiate(_SpellsPanel, _Canvas.transform).GetComponent<SpellsPanelManager>();
        }

        private void Timer()
        {
            if (_RemainingTime < 0)
            {
                ExitScreen();
            }
            _RemainingText.text = "Remaining time to choose a spell: " + _RemainingTime + " seconds";
            --_RemainingTime;
        }

        void GenerateCard(Type spellType)
        {
            //var spellCardManager = cardInstance.GetComponent<SpellCardManager>();
            //spellCardManager.AuthorizeCasting = AuthorizeCasting;
            //spellCardManager.RectoTitle.text = spellManager.GetTitle();
            //spellCardManager.RectoDescription.text = spellManager.GetDescription();
            //spellCardManager.CastSpellAction += spellManager.OnCastButtonClick;
            //var sprite = Resources.Load<Sprite>(spellManager.GetSpritePath());
            //if (sprite)
            //{
            //    spellCardManager.RectoSprite.sprite = sprite;
            //    spellCardManager.VersoSprite.sprite = sprite;
            //}
        }

        #endregion
    }
}