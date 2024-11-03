using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    [SerializeField] private float[] ProgressionIntervals = new float[3] { 10,30,60 };

    [SerializeField] private float engineDamageOffsetTime = 3.0f;

    //Get the ship in various states
    [SerializeField] private Sprite[] shipStates = new Sprite[5]{ null,null,null,null,null};
    [SerializeField] private Sprite[] engineStates = new Sprite[4] { null, null, null,null };
    int currentEngineState = -1;

    private GameObject engineGO;
    private bool engineStageOffset = false;
    private float engineStageOffsetTimer = 0;
    //Timer, used for engine sprites and to increase time remaining
    private Timer timer;

    //Get the Crew Spawner
    [SerializeField] private GameObject CrewSpawnerObj;
    private CrewSpawner crewSpawner;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        //Set the timer to max time
        //TODO: Add beginning time for tutorial
        timer.setTimeRemaining(ProgressionIntervals[ProgressionIntervals.Length-1]);
        //fetches crewspawning script
        crewSpawner = CrewSpawnerObj.GetComponent<CrewSpawner>();

        engineGO = gameObject.transform.GetChild(0).gameObject;

        //Plays the Button_Click event
        AkSoundEngine.PostEvent("States_Engine", this.gameObject);
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
        else
        {
            for (int i = 0; i < ProgressionIntervals.Length; i++)
            {
                //Check which band it's in
                if (timer.getTimeLeft() < ProgressionIntervals[i] && !engineStageOffset)
                {
                    //If last frame it wasn't below the interval
                    if (timer.getTimeLeftLF() >= ProgressionIntervals[i])
                    {
                        Debug.Log("Progressed to " + i);
                        SetEngineState(i);
                    }
                    //Don't continue
                    break;
                }
            }
        }
        

        //temporarily change engine sprite
        if(engineStageOffset)
        {
            engineStageOffsetTimer += Time.deltaTime;
            if(engineStageOffsetTimer > engineDamageOffsetTime)
            {
                engineStageOffset = false;
                engineStageOffsetTimer = 0;
                Debug.Log("Returned to " + (currentEngineState - 1));
                SetEngineState(currentEngineState - 1);
            }
        }
    }

    //Called on beginning of death anim
    private void DeathBegin()
    {
        dying = true;
        SetEngineState(engineStates.Length - 1);
    }
    //Caled on end of death anim
    private void DeathEnd() 
    {
        //Death stuff

        //Plays the Player_Damaged event
        AkSoundEngine.PostEvent("Player_Death", this.gameObject);
        //Sets the "Music" State Group's active State to "Death"
        AkSoundEngine.SetState("Music", "Death");
        //Sets the "Engine" State Group's active State to "Stage1"
        AkSoundEngine.SetState("Engine", "Stage1");

        //TEMP
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

    //Change the audio and visual of the ship
    private void SetShipDamamgeState(int newState)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = shipStates[newState];
    }

    private void SetEngineState(int newState)
    {
        string[] audioNames = new string[4] { "Stage4", "Stage3", "Stage2", "Stage1" };
        if(newState == currentEngineState) { return; }

        currentEngineState = newState;

        engineGO.GetComponent<SpriteRenderer>().sprite = engineStates[newState];
        AkSoundEngine.SetState("Engine", audioNames[newState]);
    }

    public string GetEngineState()
    {
        string[] audioNames = new string[4] { "Stage4", "Stage3", "Stage2", "Stage1" };
        return audioNames[currentEngineState];
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


            collision.gameObject.GetComponent<MeteorPhysics>().Explode();

            lives--;

            //Temporarily make engine state one less
            if(!engineStageOffset && currentEngineState < 3)
            {
                Debug.Log("Temporary downgrade to " + (currentEngineState + 1));
                SetEngineState(currentEngineState + 1);
            }
            engineStageOffset = true;
            engineStageOffsetTimer = 0;


            //Plays the Player_Damaged event
            AkSoundEngine.PostEvent("Player_Damaged", this.gameObject);

            //triggers crew spawn on collision
            crewSpawner.SpawnCrew();
            for (int i = 0; i < ProgressionIntervals.Length; ++i)
            {
                if (ProgressionIntervals[i] > timer.getTimeLeft())
                {
                    timer.setTimeRemaining(ProgressionIntervals[i]);
                    break;
                }
            }

            SetShipDamamgeState(lives);
            if(lives==0)
            {
                DeathBegin();
            }

        }
    }

   
}
