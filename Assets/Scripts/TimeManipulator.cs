using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Denotes an action in a point of time which when reached will manipulate the time.
/// </summary>
public abstract class TimeManipulator : MonoBehaviour
{
    public float EffectTime;

    void Start()
    {
        TimeLine.AddTimelineManipulator(this);
    }

    public abstract void OnReached();


}