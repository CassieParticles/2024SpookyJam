using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;

    private GameObject canvas;
    private GameObject ship;

    public bool getPaused()
    {
        return paused;
    }

    private void Start()
    {
        ship = GameObject.Find("Ship");
        canvas = gameObject.transform.GetChild(0).gameObject;
        canvas.SetActive(false);
        paused = false;

        //Sets the "Music" State Group's active State to "Gameplay"
        AkSoundEngine.SetState("Music", "Gameplay");
        //Sets the "Engine" State Group's active State to "Stage1"
        AkSoundEngine.SetState("Engine", "Stage1");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("WinScreen").activeSelf && !GameObject.Find("LoseScreen").activeSelf)
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
        //Sets the "Music" State Group's active State to "Gameplay"
        AkSoundEngine.SetState("Music", "Gameplay");
        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Pause_Menu_Close", this.gameObject);
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
            //Sets the "Engine" State Group's active State to "Stage1"
            AkSoundEngine.SetState("Engine", "Stage1");
            //Plays the Pause_Menu_Open event
            AkSoundEngine.PostEvent("Pause_Menu_Open", this.gameObject);
        }
        else
        {
            //Sets the "Music" State Group's active State to "Gameplay"
            AkSoundEngine.SetState("Music", "Gameplay");
            //Sets the "Engine" State Group's active State to correspond with the EngineState from the ship.cs script.
            AkSoundEngine.SetState("Engine", ship.GetComponent<Ship>().GetEngineState());
            //Plays the Pause_Menu_Close event
            AkSoundEngine.PostEvent("Pause_Menu_Close", this.gameObject);
        }
    }
    
}
