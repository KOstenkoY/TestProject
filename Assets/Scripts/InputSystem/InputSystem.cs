using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static event Action<bool> OnTouchDetection;

    public static event Action OnRestartGame;
    public static event Action OnExitGame;
    public static event Action<bool> OnSetPause;

    protected void TouchDetected(bool isTouch)
    {
        OnTouchDetection?.Invoke(isTouch);
    }

    protected void RestartPressed()
    {
        OnRestartGame?.Invoke();
    }

    protected void ExitGame()
    {
        OnExitGame?.Invoke();
    }

    protected void SetPause(bool isPressed)
    {
        OnSetPause?.Invoke(isPressed);
    }
}
