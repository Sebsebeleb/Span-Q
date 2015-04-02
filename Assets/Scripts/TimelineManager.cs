using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    private bool IsSimulating;
    private GameLevelManager levelManager;

    private void Start()
    {
        Time.timeScale = 0f;
        levelManager = GameObject.FindWithTag("GameLevel").GetComponent<GameLevelManager>();
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
        if (b)
        {
            StartSimulating();
        }
        else
        {
            StopSimulating();
        }
        IsSimulating = b;
    }

    private void StartSimulating()
    {
        TimeLine.StartPlayback();
        GameObject.FindWithTag("TimelineManager").BroadcastMessage("OnSimulationStart");
    }

    private void StopSimulating()
    {
        TimeLine.StopPlayback();
        TimeLine.CurrentTime = 0f;
        levelManager.ResetLevel();
    }
}