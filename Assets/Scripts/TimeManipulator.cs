using UnityEngine;

[RequireComponent(typeof(DragTest))]
/// <summary>
/// Denotes an action in a point of time which when reached will manipulate the time.
/// </summary>
public abstract class TimeManipulator : MonoBehaviour
{
    private DragTest drag;
    public float EffectTime;

    private void Start()
    {
        drag = GetComponent<DragTest>();
    }

    public abstract void OnReached();

    /// TODO: should be called when simulation starts
    public void OnSimulationStart()
    {
        if (drag.IsSnapped)
        {
            TimeLine.AddTimelineManipulator(this);
        }
    }

}