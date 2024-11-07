using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{

    private GameObject canvas;
    private GameObject ship;
    private float timer;
    private bool winning = false;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Ship");
        canvas = gameObject.transform.GetChild(0).gameObject;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (winning) {
            timer += Time.deltaTime;
            if (timer > 10) {
                canvas.SetActive(true);
            }
        }
    }

    public void Win() {
        winning = true;
    }

    public void Retry() {
        SceneManager.LoadScene("GameScene");

        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
    }

    public void Quit() {
        SceneManager.LoadScene("MainMenuScene");

        //Sets the "Music" State Group's active State to "Menu"
        AkSoundEngine.SetState("Music", "Menu");
        //Sets the "Engine" State Group's active State to "Stage1"
        AkSoundEngine.SetState("Engine", "Stage1");
        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
    }
}
