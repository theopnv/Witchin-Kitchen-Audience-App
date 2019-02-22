using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
