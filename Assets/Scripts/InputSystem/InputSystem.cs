using System;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static event Action<bool> OnTouchDetection;

    protected void TouchDetected(bool isTouch)
    {
        OnTouchDetection.Invoke(isTouch);
    }
}
