using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private bool musicPlaying;
    public static MusicManager instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            DestroyImmediate(this.gameObject);
        } else {
            instance = this;
            musicPlaying = false;
            DontDestroyOnLoad(this);
        }
    }

    public bool GetPlaying() {
        return musicPlaying;
    }

    public void SetPlaying(bool play) {
        musicPlaying = play;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
