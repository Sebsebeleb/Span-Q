using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    public bool IsActive = false; //Switched in start

    // Use this for initialization
    private void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Toggle()
    {
        IsActive = !IsActive;
        UpdateState();
    }

    private void UpdateState()
    {
        if (IsActive)
        {
            ToggleOn(transform);
        }
        else
        {
            ToggleOff(transform);
        }
    }

    private void ToggleOff(Transform door)
    {
        SpriteRenderer doorRenderer = door.GetComponent<SpriteRenderer>();

        door.GetComponent<Collider2D>().enabled = false;
        Color nextColor = new Color(doorRenderer.color.r, doorRenderer.color.g, doorRenderer.color.b, 0.3f);
        door.GetComponent<SpriteRenderer>().color = nextColor;
    }

    private void ToggleOn(Transform door)
    {
        SpriteRenderer doorRenderer = door.GetComponent<SpriteRenderer>();

        door.GetComponent<Collider2D>().enabled = true;
        Color nextColor = new Color(doorRenderer.color.r, doorRenderer.color.g, doorRenderer.color.b, 1f);
        door.GetComponent<SpriteRenderer>().color = nextColor;
    }
}