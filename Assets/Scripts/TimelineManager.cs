using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    private bool IsSimulating;

    private void Start()
    {
    }


    private void Update()
    {
        if (IsSimulating)
        {
            TimeLine.SimulateTime();
        }
    }

    public void SetSimulating(bool b)
    {
        IsSimulating = b;
        GameObject.FindWithTag("TimelineSystem").BroadcastMessage("OnSimulationStart");
    }
}