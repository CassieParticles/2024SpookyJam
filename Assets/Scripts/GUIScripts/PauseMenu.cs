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

        //Sets the "Music" State Group's active State to "Gameplay"
        AkSoundEngine.SetState("Music", "Gameplay");

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            canvas.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
            MusicStates(paused);
        }
    }


    public void Unpause()
    {
        paused = false;
        canvas.SetActive(false);
        Time.timeScale = 1;

        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
    }

    public void QuitToMenu()
    {
        //TODO: Quit

        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
    }

    public void MusicStates(bool state)
    {
        if (state == true)
        {
            //Sets the "Music" State Group's active State to "Menu"
            AkSoundEngine.SetState("Music", "Menu");
        }
        else
        {
            //Sets the "Music" State Group's active State to "Gameplay"
            AkSoundEngine.SetState("Music", "Gameplay");
        }
    }
    
}
