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

        public GameObject Verso;
        public Button[] VersoPlayerButtons = new Button[2];

        [HideInInspector] public Action<int> CastSpellAction;

        private bool _RotateRecto;
        private bool _RotateVerso;
        private float _RotationSpeed = 125f; // Magic number (a cleaner way would be to calculate the speed depending of the rotation duration).
        #region Unity API

        void Start()
        {
            if (!AuthorizeCasting)
            {
                RectoCastSpellButton.gameObject.SetActive(false);
            }
            // Shitty API that doesn't allow simple parameter binding in a for loop
            VersoPlayerButtons[0].onClick.AddListener(delegate { OnTargetButtonClick(0); });
            VersoPlayerButtons[1].onClick.AddListener(delegate { OnTargetButtonClick(1); });
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
            Debug.Log("Button number " + targetId);
            CastSpellAction?.Invoke(targetId);
        }

        #endregion
    }

}
