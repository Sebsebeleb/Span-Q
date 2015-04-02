using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(DragTest))]
/// <summary>
/// Displays the time this manipulator (or event?) is supposed to be fired off
/// </summary>
public class ManipulatorTextDisplayBehaviour : MonoBehaviour
{
    public Text snapText;
    private DragTest drag;

    private void Start()
    {
        drag = GetComponent<DragTest>();
    }

    private void Update()
    {
        if (!drag.IsSnapped) {
            snapText.text = "";
        }
        else {
            snapText.text = string.Format("{0:F1}", drag.SnappedAt);
        }

    }
}
