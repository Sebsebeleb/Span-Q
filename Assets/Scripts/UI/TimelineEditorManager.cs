using UnityEngine;

public class TimelineEditorManager : MonoBehaviour
{
    public GameObject EventDisplayPrefab;
    public GameObject EditAreaPanel;
    public Transform MinTransform;
    public Transform MaxTransform;

    private RectTransform editAreaRect;

    public float NumberOfSnapPoints;
    public float TimePerSnap;


    public float MinTime;
    public float MaxTime;

    void Start()
    {
        editAreaRect = EditAreaPanel.GetComponent<RectTransform>();
    }

    void Update()
    {

    }

    public void RegisterTimelineEvent(TimelineEventTrigger eventTrigger)
    {
        Vector3 position = calculatePosition(eventTrigger.TriggerTime);

        GameObject eventDisplay = Instantiate(EventDisplayPrefab, position, Quaternion.identity) as GameObject;
        eventDisplay.transform.SetParent(transform);
    }

    private Vector3 calculatePosition(float triggerTime)
    {
        float f = triggerTime / (MaxTime - MinTime);

        Vector3 returnPosition = new Vector3(MaxTransform.position.x, MaxTransform.position.y, MaxTransform.position.z);
        returnPosition.x = MaxTransform.position.x * f + MinTransform.position.x;
        returnPosition.y -= 100f;

        return returnPosition;
    }

    public bool InEditZone(Vector3 position)
    {
        Vector3[] rectWorld = new Vector3[4];
        editAreaRect.GetWorldCorners(rectWorld);
        Rect worldRect = getWorldRect(); 
        return worldRect.Contains(position);
    }

    private Rect getWorldRect()
    {
        Vector3[] rectWorld = new Vector3[4];
        editAreaRect.GetWorldCorners(rectWorld);
        Rect worldRect = new Rect(rectWorld[0].x, rectWorld[3].y, rectWorld[2].x, rectWorld[1].y);

        return worldRect;

    }

    public void Snap(GameObject eventObject)
    {
        Vector3 eventPosition = eventObject.transform.position;
        Rect worldRect = getWorldRect();

        // The position relative from start of timeline
        Vector3 relativePosition = eventPosition;
        relativePosition.x -= worldRect.left;

        // The actual size of the edit rect
        float xSize = worldRect.xMax - worldRect.xMin;
        // Find out what percentage we are on the timeline
        float f = relativePosition.x = relativePosition.x / xSize;
        //Now find snap point based on number of them
        float snap = Mathf.Round(f * NumberOfSnapPoints);

        // transform size of each snap
        float sizeOfEachSnap = xSize / NumberOfSnapPoints;
        eventObject.transform.position = new Vector3((snap * sizeOfEachSnap) + worldRect.left, relativePosition.y, relativePosition.z);

        // Call snap event callback
        float snaptime = snap * TimePerSnap;
        gameObject.BroadcastMessage("OnSnapped", snaptime);
    }
}