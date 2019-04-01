using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace audience.game
{

    public class SpellCardManager : MonoBehaviour
    {
        public bool AuthorizeCasting;

        public GameObject Recto;
        public Text RectoTitle;
        public Text RectoDescription;
        public Button RectoCastSpellButton;
        public Image RectoSprite;
        public Image VersoSprite;

        public GameObject Verso;

        [SerializeField] private GameObject _ButtonsPlaceholder;
        [SerializeField] private GameObject _TargetPlayerButton;

        [HideInInspector] public Action<int> CastSpellAction;

        private bool _RotateRecto;
        private bool _RotateVerso;
        private float _RotationSpeed = 125f; // Magic number (a cleaner way would be to calculate the speed depending of the rotation duration).
        private bool _clicked;

        #region Unity API

        void Start()
        {
            _clicked = false;
            if (!AuthorizeCasting)
            {
                RectoCastSpellButton.gameObject.SetActive(false);
            }

            SetButtons();
        }

        void SetButtons()
        {
            //Shitty Button API that doesn't allow simple parameter binding in a for loop
            var i = 0;
            if (GameInfo.PlayerNumber > i)
            {
                InstantiateButton(i, GameInfo.PlayerColors[i]);
            }

            ++i;
            if (GameInfo.PlayerNumber > i)
            {
                InstantiateButton(i, GameInfo.PlayerColors[i]);
            }

            ++i;
            if (GameInfo.PlayerNumber > i)
            {
                InstantiateButton(i, GameInfo.PlayerColors[i]);
            }

            ++i;
            if (GameInfo.PlayerNumber > i)
            {
                InstantiateButton(i, GameInfo.PlayerColors[i]);
            }

        }

        void InstantiateButton(int i, Color color)
        {
            var instance = Instantiate(_TargetPlayerButton, _ButtonsPlaceholder.transform);
            var button = instance.GetComponent<Button>();
            button.GetComponent<Image>().color = color;
            button.onClick.AddListener(delegate { OnTargetButtonClick(i); });
            var manager = button.GetComponent<TargetPlayerButtonManager>();
            manager.Player.text = GameInfo.PlayerNames[i];
            manager.Score.text = GameInfo.PlayerPotions[i].ToString();
        }

        void Update()
        {
            if (_RotateRecto)
            {
                if (Recto.transform.eulerAngles.y < 90)
                {
                    Recto.transform.Rotate(new Vector3(0, _RotationSpeed * Time.deltaTime, 0));
                }
                else
                {
                    Recto.SetActive(false);
                    _RotateRecto = false;
                    Verso.SetActive(true);
                    _RotateVerso = true;
                }
            }

            if (_RotateVerso)
            {
                if (Verso.transform.eulerAngles.y >= 270 && Verso.transform.eulerAngles.y < 360)
                {
                    Verso.transform.Rotate(new Vector3(0, _RotationSpeed * Time.deltaTime, 0));
                }
                else
                {
                    _RotateVerso = false;
                }
            }
        }

        #endregion

        #region Custom Methods

        public void OnCastSpellButtonClick()
        {
            _RotateRecto = true;
        }

        public void OnTargetButtonClick(int targetId)
        {
            if (!_clicked)
            {
                Debug.Log("Button number " + targetId);
                CastSpellAction?.Invoke(targetId);
                _clicked = true;
            }
        }

        #endregion
    }

}
