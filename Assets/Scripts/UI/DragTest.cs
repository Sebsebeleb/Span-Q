using UnityEngine;
using UnityEngine.EventSystems;

public class DragTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //If it is snapped, something should make sure to add it to the timeline data
    public bool IsSnapped = false;
    public float SnappedAt;

    private TimelineEditorManager timelineManager;
    private TimeManipulator manipulator;

    public void Start()
    {
        manipulator = GetComponent<TimeManipulator>();
        timelineManager = GameObject.FindWithTag("TimelineManager").GetComponent<TimelineEditorManager>();
    }

    // TODO: Disallow dragging while simulation is playing
    public void DragPosition(Vector3 dragPosition)
    {

        if (TimeLine.IsSimulating) {
            return;
        }

        // We set it first because the snapping uses its position, and we need to override it in case of snap
        transform.position = dragPosition;

        if (timelineManager.InEditZone(dragPosition))
        {
            timelineManager.Snap(gameObject);
            IsSnapped = true;
            manipulator.EffectTime = SnappedAt;
        }
        else
        {
            IsSnapped = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnSnapped(float snapTime)
    {
        SnappedAt = snapTime;
    }
}