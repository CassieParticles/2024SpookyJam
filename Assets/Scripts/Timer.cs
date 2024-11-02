using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //TODO: Add stuff for tutorial time
    private float timeLeft = 0;

    private float timeElapsed = 0;

    public float getTimeLeft()
    {
        return timeLeft;
    }

    //Get the time since the timer started
    public float getTimeElapsed()
    {
        return timeElapsed;
    }

    public void setTimeRemaining(float timeRemaining)
    {
        timeLeft = timeRemaining;
    }

    private void Update()
    {
        timeLeft-=(float) Time.deltaTime;
        timeElapsed += Time.deltaTime;
    }
}
