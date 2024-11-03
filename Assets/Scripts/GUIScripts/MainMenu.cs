using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //Plays the States_Music event
        AkSoundEngine.PostEvent("States_Music", this.gameObject);
        //Sets the "Music" State Group's active State to "Menu"
        AkSoundEngine.SetState("Music", "Menu");
    }

    public void PlayGame()
    {
        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
        //Sets the "Music" State Group's active State to "Gameplay"
        AkSoundEngine.SetState("Music", "Gameplay");

        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);

        Application.Quit();
    }
}
