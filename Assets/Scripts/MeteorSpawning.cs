using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorSpawning : MonoBehaviour
{

    [SerializeField][Range(0.1f, 20)] private float spawnFreq;
    private float spawnTime;
    [SerializeField] private GameObject meteor;
    private float spawnAngle, spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnFreq) {
            spawnTime -= spawnFreq;

            //spawnAngle = some random thingy(0, 2 * math.PI);


            Instantiate(meteor); //Need to have position and velocity as parameters
        }
    }
}
