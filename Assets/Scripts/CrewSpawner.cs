using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSpawner : MonoBehaviour
{

    [SerializeField] private GameObject crew;
    [SerializeField] private int maxCrew;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCrew()
    {

        for (int i = 0; i < maxCrew; i++)
        {
            GameObject newCrew = Instantiate(crew, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
           
        }
        
            timer = 5.0f;
       
    }




}
