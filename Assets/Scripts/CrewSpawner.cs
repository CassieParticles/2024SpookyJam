using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSpawner : MonoBehaviour
{

    [SerializeField] private GameObject crew;
    [SerializeField] private int MaxCrew;
    private int crewNumber;

    // Update is called once per frame
    void Update()
    {
        SpawnCrew();
    }

    void SpawnCrew()
    {
        crewNumber = MaxCrew;
        if (crewNumber > 0)
            {
            GameObject newCrew = Instantiate(crew);
        }
       

    }




}
