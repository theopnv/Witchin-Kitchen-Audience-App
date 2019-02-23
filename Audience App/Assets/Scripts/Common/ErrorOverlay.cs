using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace audience
{
    public class ErrorOverlay : MonoBehaviour
    {
        public Text ErrorWindowText;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(gameObject);
            }
        }
    }

}
