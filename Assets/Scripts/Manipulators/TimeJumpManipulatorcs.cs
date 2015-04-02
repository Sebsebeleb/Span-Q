
public class TimeJumpManipulatorcs : TimeManipulator
{

    public float JumpTime;

    public override void OnReached()
    {
        TimeLine.CurrentTime = TimeLine.CurrentTime + JumpTime;
    }

}