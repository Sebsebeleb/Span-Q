using UnityEngine;

public class TimelineMarkerMover : MonoBehaviour
{

    public TimelineEditorManager Manager;

    void Start()
    {
        Manager = GetComponentInParent<TimelineEditorManager>();
    }

    void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        float f = TimeLine.CurrentTime / (Manager.MaxTime - Manager.MinTime); //Figure out the factor of where we are on the timeline

        //Set our position based on that factor
        Vector3 newPosition = new Vector3(Manager.MaxTransform.position.x, Manager.MaxTransform.position.y, Manager.MaxTransform.position.z);
        newPosition.x = Manager.MaxTransform.position.x * f + Manager.MinTransform.position.x;

        transform.position = newPosition;
    }
}