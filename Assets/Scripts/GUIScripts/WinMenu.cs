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
    }

    public void Quit() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
