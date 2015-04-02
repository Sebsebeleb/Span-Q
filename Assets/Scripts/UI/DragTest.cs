using UnityEngine;
using UnityEngine.EventSystems;

public class DragTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //If it is snapped, something should make sure to add it to the timeline data
    public bool IsSnapped = false;
    public float SnappedAt;

    private TimelineEditorManager timelineManager;

    public void Start()
    {
        timelineManager = GameObject.FindWithTag("TimelineManager").GetComponent<TimelineEditorManager>();
    }

    public void DragPosition(Vector3 dragPosition)
    {
        // We set it first because the snapping uses its position, and we need to override it in case of snap
        transform.position = dragPosition;

        if (timelineManager.InEditZone(dragPosition))
        {
            timelineManager.Snap(gameObject);
            IsSnapped = true;
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