public class TouchDetection : InputSystem
{
    public void TouchPressed(bool isTouch)
    {
        TouchDetected(isTouch);
    }

    public void OnRestartButtonPressed()
    {
        RestartPressed();
    }

    public void OnExitButtonPressed()
    {
        ExitGame();
    }

    public void OnPauseButtonPressed(bool isPressed)
    {
        SetPause(isPressed);
    }
}
