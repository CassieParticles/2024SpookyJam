using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{
    [SerializeField] private float randomOffsetRange = 10;
    [SerializeField] private float meteorInitialSpeed = 3;
    
    //Meteors per second
    [SerializeField] private float initialSpawnRate = 0.5f;
    [SerializeField] private float spawnRateIncrease = 0.1f;
    [SerializeField] private float maxSpawnRate = 10;

    [SerializeField] private GameObject meteor;
    private float spawnTime;
    private float spawnDistance;

    private Timer timer;

    void SpawnMeteor()
    {
        float spawnAngle = UnityEngine.Random.value * 2 * 3.14159f;  //Get random angle in radians

        Vector2 generationDirection = new Vector2(Mathf.Cos(spawnAngle),Mathf.Sin(spawnAngle));
        generationDirection *= spawnDistance;

        float moveAngle = spawnAngle + 3.14159f;    //Rotate spawn angle by PI radians (180 degrees) to get direction of ship

        //Generate random number between 0 -> 1, rescale to be between -1 -> 1, then multiply by randomOffsetRange to be between -randomOffsetRange -> randomOffsetRange, then convert to radians
        float offset = (UnityEngine.Random.value * 2 - 1) * randomOffsetRange  * 3.14159f / 180;
        moveAngle += offset;

        Vector2 moveDirection = new Vector2(Mathf.Cos(moveAngle), Mathf.Sin(moveAngle)) * meteorInitialSpeed;

        GameObject newMeteor = Instantiate(meteor);
        newMeteor.transform.position = generationDirection;
        newMeteor.GetComponent<Rigidbody2D>().velocity = moveDirection;


        newMeteor.GetComponent<MeteorPhysics>().SetDespawnRange(spawnDistance);
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;

        float aspect = (float)Screen.width / Screen.height;
        spawnDistance = new Vector2(Camera.main.orthographicSize  * aspect, Camera.main.orthographicSize).magnitude; //This can be moved to update for dynamic changes in resolution but wasnt sure if it was worth it for doing this calculation every frame

        timer = GameObject.Find("Timer").GetComponent<Timer>();

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: When timer is added, adjust meteor spawn rate
        spawnTime += Time.deltaTime;

        float spawnRate = initialSpawnRate + spawnRateIncrease * timer.getTimeElapsed();
        float spawnFreq = 1.0f / spawnRate;

        if(spawnTime > spawnFreq)
        {
            spawnTime -= spawnFreq;
            SpawnMeteor();
        }
    }
}
