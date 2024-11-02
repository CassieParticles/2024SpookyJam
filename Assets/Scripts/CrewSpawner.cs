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
        timer = timer - Time.deltaTime;
        if (timer < 0)
        {
            SpawnCrew();
            
        }
    }

    public void SpawnCrew()
    {

        for (int i = 0; i < maxCrew; i++)
        {
            GameObject newCrew = Instantiate(crew);
           
        }
        
            timer = 5.0f;
       
    }




}
