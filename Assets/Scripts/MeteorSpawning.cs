using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{
    [SerializeField] private float spawnDistance = 10;   //Figure out the spawn distance
    [SerializeField] private float randomOffsetRange = 20;
    [SerializeField] private float meteorInitialSpeed = 5;


    [SerializeField][Range(0.1f, 20)] private float spawnFreq;
    private float spawnTime;
    [SerializeField] private GameObject meteor;
    private float spawnAngle, spawnPos;

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


        newMeteor.GetComponent<MeteorPhysics>().setDespawnRange(spawnDistance);
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: When timer is added, adjust meteor spawn rate
        spawnTime += Time.deltaTime;

        if(spawnTime > spawnFreq)
        {
            spawnTime -= spawnFreq;
            SpawnMeteor();
        }
    }
}
