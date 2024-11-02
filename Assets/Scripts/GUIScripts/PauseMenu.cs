using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;

    private GameObject canvas;

    public bool getPaused()
    {
        return paused;
    }

    private void Start()
    {
        canvas = gameObject.transform.GetChild(0).gameObject;
        canvas.SetActive(false);
        paused = false;
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            canvas.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;

            AkSoundEngine.SetState("Music", "Menu");
        }
    }


    public void Unpause()
    {
        paused = false;
        canvas.SetActive(false);
        Time.timeScale = 1;

        AkSoundEngine.SetState("Music", "Gameplay");
    }

    public void QuitToMenu()
    {
        //TODO: Quit
    }
    
}
