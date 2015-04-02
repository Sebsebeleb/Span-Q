using UnityEngine;

public class TimelineEventTrigger : MonoBehaviour
{
    public float TriggerTime;
    public UnityEngine.Events.UnityEvent OnTriggered;

    void Start()
    {
        TimeLine.AddTimelineEvent(this);
    }

    public void Trigger()
    {
        OnTriggered.Invoke();
    }
}