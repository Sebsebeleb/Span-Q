using System;
using System.Collections.Generic;
using UnityEngine;

public static class TimeLine
{
    public static float EndTime;
    public static float StartTime;

    private static float _currentCurrentTime;

    public static float CurrentTime
    {
        get { return _currentCurrentTime; }
        set { _currentCurrentTime = value; }
    }

    /// Events that manipulate the timeline. Key: the time stamp on timeline when it is triggered, value: what is triggered
    public static List<KeyValuePair<float, TimeManipulator>> TimemanipulatorEvents = new List<KeyValuePair<float, TimeManipulator>>();

    /// Events that are raised as part of the timeline reaching the point
    public static List<KeyValuePair<float, TimelineEventTrigger>> TimelineEvents = new List<KeyValuePair<float, TimelineEventTrigger>>();

    static void StartPlayback()
    {
        _currentCurrentTime = StartTime;
    }

    public static void SimulateTime()
    {

        float deltaTime = Time.deltaTime;

        while (deltaTime != 0f)
        {
            if (deltaTime <= 0f)
            {
                throw new Exception("Error in manipulation of time. More time was spent than it was given to spend");
            }

            float timePassed = UpdateToNextEvent(deltaTime);
            UpdateTime(timePassed);
            deltaTime -= timePassed;

        }
    }

    public static void AddTimelineEvent(TimelineEventTrigger ev)
    {
        TimelineEvents.Add(new KeyValuePair<float, TimelineEventTrigger>(ev.TriggerTime, ev));
    }

    public static void AddTimelineManipulator(TimeManipulator manipulator)
    {
        
        TimemanipulatorEvents.Add(new KeyValuePair<float, TimeManipulator>(manipulator.EffectTime, manipulator));
    }
    /// <summary>
    /// Updates the advancing of time on timeline
    /// </summary>
    /// <param name="timePassed"></param>
    private static void UpdateTime(float timePassed)
    {
        CurrentTime += timePassed;
    }


    /// <summary>
    /// Moves as far into the timeline as it can untill it reaches an event, and plays that event
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>The amount of time that passed untill manipulator was reached</returns>
    private static float UpdateToNextEvent(float deltaTime)
    {
        KeyValuePair<float, TimeManipulator> earliestManipulator = new KeyValuePair<float, TimeManipulator>();
        bool found = false;
        foreach (KeyValuePair<float, TimeManipulator> pair in TimemanipulatorEvents)
        {

            float eventTime = pair.Key;
            if (passedThisFrame(eventTime))
            {
                earliestManipulator = pair;
                found = true;
            }
        }

        if (!found)
        {
            // No manipulators were reached
            return deltaTime;
        }



        float manipulatorTime = earliestManipulator.Key;

        TriggerManipulator(earliestManipulator.Value);

        return deltaTime - (manipulatorTime - CurrentTime);
    }

    private static void TriggerManipulator(TimeManipulator timeManipulator)
    {
        timeManipulator.OnReached();
    }

    /// <summary>
    /// Did we pass this mark this frame?
    /// </summary>
    /// <param name="eventTime"></param>
    /// <returns></returns>
    private static bool passedThisFrame(float eventTime)
    {
        if (eventTime > _currentCurrentTime && _currentCurrentTime + Time.deltaTime > eventTime)
        {
            return true;
        }
        return false;
    }
}
