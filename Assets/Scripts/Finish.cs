using UnityEngine;

public class Finish : MonoBehaviour
{
    public static event System.Action OnFinishGame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnFinishGame?.Invoke();
        }
    }
}
