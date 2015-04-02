
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeJumpManipulatorcs : TimeManipulator
{
    public InputField JumpTimeInput;
    public Text JumpTimeText;
    public Text MinValueText;
    public Text MaxValueText;

    public float JumpTime;

    public float MinValue;
    public float MaxValue;

    public void Start()
    {
        MinValueText.text = MinValue.ToString();
        MaxValueText.text = MaxValue.ToString();
        JumpTime = Mathf.Clamp(JumpTime, MinValue, MaxValue);
    }

    public override void OnReached()
    {
        TimeLine.CurrentTime = TimeLine.CurrentTime + JumpTime;
    }

    public void Update()
    {
        if (JumpTimeText.text != JumpTime.ToString() && !JumpTimeInput.isFocused)
        {
            JumpTimeInput.text = JumpTime.ToString();
            JumpTimeText.text = JumpTime.ToString();
        }
    }

    public void TrySetValue(string value)
    {
        double realDouble;
        int realInt;
        bool successDouble = double.TryParse(value, out realDouble);
        bool successInt = int.TryParse(value, out realInt);

        if ((successDouble || successInt))
        {
            if (successDouble)
            {
                JumpTime = Mathf.Clamp((float)realDouble, MinValue, MaxValue);
                // Round it to 1 decimal
                JumpTime = (float)Math.Round(JumpTime, 1);
            }
            if (successInt)
            {
                JumpTime = Mathf.Clamp(realInt, MinValue, MaxValue);
            }
        }

    }

}