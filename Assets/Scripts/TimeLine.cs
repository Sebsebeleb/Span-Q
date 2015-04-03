using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public static class TimeLine
{

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

    // The scale of how fast the timeline passes
    private static float _timescale = 1.0f;

    public static void StartPlayback()
    {
        Time.timeScale = 1f;
        _currentCurrentTime = 0;
    }

    // TODO: Call me maybe
    public static void StopPlayback()
    {
        Time.timeScale = 0f;
        TimelineEvents.Clear();
        TimemanipulatorEvents.Clear();
        Reset();
    }

    public static void SimulateTime()
    {

        float deltaTime = Time.deltaTime * _timescale;

        while (deltaTime != 0f)
        {
            if (deltaTime <= 0f)
            {
                throw new Exception("Error in manipulation of time. More time was spent than it was given to spend");
            }

            float timePassed = UpdateToNextManipulator(deltaTime);
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
    /// Remove a manipulator from the timeline. Used for stuff like one-off manipulators.
    /// </summary>
    /// <param name="manipulatorToRemove">The manipulator trigger to remove</param>
    public static void RemoveTimelineManipulator(TimeManipulator manipulatorToRemove)
    {
        KeyValuePair<float, TimeManipulator> toRemove = new KeyValuePair<float, TimeManipulator>();
        bool found = false;
        // bad and lazy way
        foreach (KeyValuePair<float, TimeManipulator> pair in TimemanipulatorEvents)
        {
            if (pair.Value == manipulatorToRemove)
            {
                toRemove = pair;
                found = true;
                break;
            }
        }

        if (found)
        {
            TimemanipulatorEvents.Remove(toRemove);
        }
    }
    /// <summary>
    /// Updates the advancing of time on timeline
    /// </summary>
    /// <param name="timePassed"></param>
    private static void UpdateTime(float timePassed)
    {
        CheckAndFireEvents(timePassed);
        CurrentTime += timePassed;

    }

    /// <summary>
    /// Fires off all TimelineEvents that took place in the frame of time that passed
    /// </summary>
    /// <param name="timePassed"></param>
    private static void CheckAndFireEvents(float timePassed)
    {
        foreach (KeyValuePair<float, TimelineEventTrigger> pair in TimelineEvents)
        {
            float eventTime = pair.Key;

            if (passedThisFrame(eventTime, timePassed))
            {
                pair.Value.Trigger();
            }
        }
    }


    /// <summary>
    /// Moves as far into the timeline as it can untill it reaches an event, and plays that event
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>The amount of time that passed untill manipulator was reached</returns>
    private static float UpdateToNextManipulator(float deltaTime)
    {
        KeyValuePair<float, TimeManipulator> earliestManipulator = new KeyValuePair<float, TimeManipulator>();
        bool found = false;
        foreach (KeyValuePair<float, TimeManipulator> pair in TimemanipulatorEvents)
        {

            float eventTime = pair.Key;
            if (passedThisFrame(eventTime, deltaTime))
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

        // We need to return the amount of time passed
        float oldCurrentTime = CurrentTime;

        TriggerManipulator(earliestManipulator.Value);

        return deltaTime - (manipulatorTime - oldCurrentTime);
    }

    private static void TriggerManipulator(TimeManipulator timeManipulator)
    {
        timeManipulator.OnReached();
        if (timeManipulator.UsageType == ManipulatorUsageCount.Single) {
            RemoveTimelineManipulator(timeManipulator);

            //Update its display color
            Image im = timeManipulator.GetComponent<Image>();
            Color nColor = im.color;
            nColor.a = 0.1f;
            im.color = nColor;
        }
    }

    /// <summary>
    /// Did we pass this mark this frame?
    /// </summary>
    /// <param name="eventTime"></param>
    /// <returns></returns>
    private static bool passedThisFrame(float eventTime, float deltaTime)
    {
        if (eventTime > _currentCurrentTime && _currentCurrentTime + deltaTime > eventTime)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Reset all to default, for loading new levels
    /// </summary>
    public static void Reset()
    {
        TimemanipulatorEvents.Clear();
        TimelineEvents.Clear();
        CurrentTime = 0;
        _timescale = 1f;
    }

    public static void SetTimeScale(float newScale)
    {
        _timescale = newScale;
    }
}
