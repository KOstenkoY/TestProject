using UnityEngine;

public class TouchDetection : InputSystem
{
    public void TouchPressed(bool isTouch)
    {
        TouchDetected(isTouch);
    }
}
