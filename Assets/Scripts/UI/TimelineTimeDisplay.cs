using System;
using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimelineTimeDisplay : MonoBehaviour
{

    public Text TextDisplay;

    void Update()
    {
        TextDisplay.text = String.Format("{0:F1}", TimeLine.CurrentTime);
    }
}