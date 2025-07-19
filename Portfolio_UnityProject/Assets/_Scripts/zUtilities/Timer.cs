using System;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private float t, tHold;
    private bool started;

    public Timer() { t = 0f; tHold = t; }
    public Timer(float currentTime)
    {
        if (currentTime >= 0)
        {
            t = currentTime;
            tHold = t;
        }
        else
        {
            throw new Exception("CurrentTime cannot go below 0.");
        }
    }

    public bool Started { get { return started; } set { started = value; } }

    public bool Running(float deltaTime) //t in this context is time left
    {
        //if timer is not below or 0, continue running
        if(!(t <= 0f))
        {
            t -= deltaTime;
            return true;
        }
        //else
        t = 0f;
        return false;
    }

    public void ResetTimer()
    {
        t = tHold;
    }


}

