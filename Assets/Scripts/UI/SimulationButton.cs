using UnityEngine;
using UnityEngine.UI;

public class SimulationButton : MonoBehaviour
{

    private bool isSimulating = false;
    private Text buttonText;

    private TimelineManager manager;


    // Use this for initialization
    private void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        manager = GameObject.FindWithTag("GM").GetComponent<TimelineManager>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Toggled()
    {
        isSimulating = !isSimulating;

        buttonText.text = isSimulating ? "Stop Simulation" : "Start Simulation";

        manager.SetSimulating(isSimulating);
    }
}
