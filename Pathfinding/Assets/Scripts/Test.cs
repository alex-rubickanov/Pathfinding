using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{
    public bool isMT;
    public Unit[] seekers;
    public UnitMT[] seekersMT;
    private void Start()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        if (!isMT)
        {
            foreach (var seeker in seekers)
            {
                seeker.Execute();
            }

            stopwatch.Stop();
            Debug.Log($"Elapsed time {stopwatch.ElapsedTicks} ticks");
        }
        else
        {
            foreach (var seeker in seekersMT)
            {
                seeker.Execute();
            }

            stopwatch.Stop();
            Debug.Log($"Elapsed time {stopwatch.ElapsedTicks} ticks");
        }
        
    }
}