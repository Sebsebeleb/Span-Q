﻿
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleManipulator : TimeManipulator
{
    public InputField ScaleTimeInput;
    public Text ScaleTimeText;
    public Text MinValueText;
    public Text MaxValueText;

    public float TimeScale;

    public float MinValue;
    public float MaxValue;

    public new void Start()
    {
        base.Start();
        MinValueText.text = MinValue.ToString();
        MaxValueText.text = MaxValue.ToString();
        TimeScale = Mathf.Clamp(TimeScale, MinValue, MaxValue);
    }

    public override void OnReached()
    {
        TimeLine.SetTimeScale(TimeScale);
    }

    public void Update()
    {
        if (ScaleTimeText.text != TimeScale.ToString() && !ScaleTimeInput.isFocused)
        {
            string text = String.Format("{0:F1}", TimeScale);
            ScaleTimeInput.text = text;
            ScaleTimeText.text = text;
        }
    }

    public void TrySetValue(string value)
    {
        //DOnt allow input while simulating

        if (TimeLine.IsSimulating)
        {
            return;
        }

        double realDouble;
        int realInt;
        bool successDouble = double.TryParse(value, out realDouble);
        bool successInt = int.TryParse(value, out realInt);

        if ((successDouble || successInt))
        {
            if (successDouble)
            {
                TimeScale = Mathf.Clamp((float)realDouble, MinValue, MaxValue);
                // Round it to 1 decimal
                TimeScale = (float)Math.Round(TimeScale, 1);
            }
            if (successInt)
            {
                TimeScale = Mathf.Clamp(realInt, MinValue, MaxValue);
            }
        }

    }

}