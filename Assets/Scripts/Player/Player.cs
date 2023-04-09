using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Vector3 _playerMaxSize = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private Vector3 _playerMinSize = Vector3.zero;

    public Vector3 PlayerMaxSize => _playerMaxSize;
    public Vector3 PlayerMinSize => _playerMinSize;

    private void Awake()
    {
        transform.localScale = _playerMaxSize;
    }
}
