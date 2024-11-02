using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;

    private GameObject canvas;

    private void Start()
    {
        canvas.active = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
