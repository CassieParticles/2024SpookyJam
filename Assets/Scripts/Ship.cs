using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int lives = 4;


    //Variables around ship being able to die
    //TODO: When death anim is implemented, get time of animation
    [SerializeField] private float deathAnimTime = 0.5f;
    private float deathTimer = 0;
    bool dying = false;

    //When the user takes damage, what the time is set to
    [SerializeField] private float[] ProgressionIntervals = new float[3] { 2,5,10 };


    //Get the ship in various states
    [SerializeField] private Sprite[] shipStates = new Sprite[5]{ null,null,null,null,null};

    //Timer, used for engine sprites and to increase time remaining
    private Timer timer;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        //Set the timer to max time
        //TODO: Add beginning time for tutorial
        timer.setTimeRemaining(ProgressionIntervals[ProgressionIntervals.Length-1]);   
    }

    private void Update()
    {
        if (dying)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer > deathAnimTime)
            {
                DeathEnd();
            }
        }
    }

    //Called on beginning of death anim
    private void DeathBegin()
    {
        dying = true;
    }
    //Caled on end of death anim
    private void DeathEnd() 
    {
        //Death stuff
        AkSoundEngine.PostEvent("Player_Death", this.gameObject);
        //TEMP
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding object is a meteor
        if(collision.gameObject.GetComponent<MeteorPhysics>())
        {
            //Early exit to prevent lives hitting -1
            if(lives==0)
            {
                return;
            }
            //Add a check if meteor is active once one exists
            //TEMP, will call meteor explode function
            Destroy(collision.gameObject);
            lives--;

            for(int i = 0; i < ProgressionIntervals.Length; ++i)
            {
                if (ProgressionIntervals[i] > timer.getTimeLeft())
                {
                    timer.setTimeRemaining(ProgressionIntervals[i]);
                }
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = shipStates[lives];
            if(lives==0)
            {
                DeathBegin();
            }

        }
    }
}
