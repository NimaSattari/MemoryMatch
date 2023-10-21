using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("TimeManager").AddComponent<TimeManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private List<Timer> timers = new List<Timer>();

    public void AddTimer(float delay, System.Action action,bool looping = false)
    {
        timers.Add(new Timer(delay, action, looping));
    }

    private void Update()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].excutionTime < Time.time)
            {
                timers[i].action?.Invoke();
                if (timers[i].looping == false)
                {
                    timers.RemoveAt(i);
                }
            }
        }
    }
}

public class Timer
{
    public float excutionTime;
    public System.Action action;
    public bool looping;
    private float delay;

    public Timer(float delay, System.Action action, bool looping = false)
    {
        excutionTime = Time.time + delay;
        this.action = action;
        this.looping = looping;
        this.delay = delay;

        if(looping)
        {
            action += () =>
            {
                excutionTime = Time.time + this.delay;
            };
        }
    }
}