using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace audience
{

    public class Overlay : MonoBehaviour
    {
        [HideInInspector]
        public Action Primary;

        [HideInInspector]
        public Action Secondary;

        [HideInInspector]
        public string Description;

        [SerializeField]
        private Text _DescriptionText;

        void Start()
        {
            _DescriptionText.text = Description;
        }

        public void OnPrimaryButtonClick()
        {
            Primary.Invoke();
        }

        public void OnSecondaryButtonClick()
        {
            Secondary.Invoke();
        }
    }

}
