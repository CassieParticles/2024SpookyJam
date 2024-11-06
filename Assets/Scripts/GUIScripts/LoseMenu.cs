using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoseMenu : MonoBehaviour
{

    private GameObject canvas;
    private GameObject ship;

    private bool losing = false;
    private float timer = 0;

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
        if (losing) {
            timer += Time.deltaTime;
            if (timer > 3) {
                canvas.SetActive(true);
            }
        }
    }

    public void Lose() {
        losing = true;
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
        //Plays the Button_Click event
        AkSoundEngine.PostEvent("Button_Click", this.gameObject);
    }
}
