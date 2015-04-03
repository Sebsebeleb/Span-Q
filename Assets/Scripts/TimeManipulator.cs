using UnityEngine;
using UnityEngine.UI;

public enum ManipulatorType
{
    Jumper, // Jumps in time
    Reverser,
    Scaler,
}

public enum ManipulatorUsageCount
{
    Single,
    Unlimited,

}

[RequireComponent(typeof(DragTest))]
/// <summary>
/// Denotes an action in a point of time which when reached will manipulate the time.
/// </summary>
public abstract class TimeManipulator : MonoBehaviour
{
    private DragTest drag;
    public float EffectTime;

    public ManipulatorUsageCount UsageType = ManipulatorUsageCount.Single;

    public void Awake()
    {
        drag = GetComponent<DragTest>();
    }

    public void Start()
    {
        if (UsageType == ManipulatorUsageCount.Single)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("TimelineManipulatorMarkerOneTime");
        }
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