using UnityEngine;

public class TimelineEventTrigger : MonoBehaviour
{
    public float TriggerTime;
    public UnityEngine.Events.UnityEvent OnTriggered;

    void Start()
    {
        TimeLine.AddTimelineEvent(this);
        GameObject.FindWithTag("TimelineManager").GetComponent<TimelineEditorManager>().RegisterTimelineEvent(this);
    }

    public void Trigger()
    {
        OnTriggered.Invoke();
    }
}