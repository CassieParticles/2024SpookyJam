using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeLeft = 90;
    private float timeMax = 90;

    public float getTimeLeft()
    {
        return timeLeft;
    }
    public void increaseTimeRemaining(float timeRemaining)
    {
        timeMax = timeRemaining;
        timeLeft = timeRemaining;
    }

    private void Update()
    {
        timeLeft-=(float) Time.deltaTime;
    }
}
