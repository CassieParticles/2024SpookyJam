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

    [SerializeField] private float engineDamageOffsetTime = 5.0f;

    //Get the ship in various states
    [SerializeField] private Sprite[] shipStates = new Sprite[5]{ null,null,null,null,null};
    [SerializeField] private Sprite[] engineStates = new Sprite[3] { null, null, null };
    int currentEngineState = -1;

    private GameObject engineGO;
    private int engineStageOffset = 0;
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

        for(int i=0;i<ProgressionIntervals.Length;i++)
        {
            //Check which band it's in
            if(timer.getTimeLeft() < ProgressionIntervals[i])
            {
                if(currentEngineState + engineStageOffset != i)
                {
                    currentEngineState = Mathf.Min(i + engineStageOffset,engineStates.Length - 1);
                    engineGO.GetComponent<SpriteRenderer>().sprite = engineStates[currentEngineState];
                }
                //Don't continue
                break;
            }
        }
        //temporarily change engine sprite
        if(engineStageOffset==1)
        {
            engineStageOffsetTimer += Time.deltaTime;
            if(engineStageOffsetTimer > engineDamageOffsetTime)
            {
                engineStageOffset = 0;
                engineStageOffsetTimer = 0;
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
            collision.gameObject.GetComponent<MeteorPhysics>().Explode();
            lives--;
            engineStageOffset = 1;
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

            gameObject.GetComponent<SpriteRenderer>().sprite = shipStates[lives];
            if(lives==0)
            {
                DeathBegin();
            }

        }
    }

   
}
