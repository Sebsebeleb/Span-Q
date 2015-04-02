using UnityEngine;

public enum ManipulatorType
{
    Jumper, // Jumps in time
    Reverser,
    Scaler,
}

[RequireComponent(typeof(DragTest))]
/// <summary>
/// Denotes an action in a point of time which when reached will manipulate the time.
/// </summary>
public abstract class TimeManipulator : MonoBehaviour
{
    private DragTest drag;
    public float EffectTime;

    private void Awake()
    {
        drag = GetComponent<DragTest>();
    }

    private void Start()
    {
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