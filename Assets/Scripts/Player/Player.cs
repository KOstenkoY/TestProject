using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Vector3 _playerMaxSize = Vector3.zero;
    [SerializeField] private Vector3 _playerMinSize = Vector3.zero;

    public Vector3 PlayerMinSize => _playerMinSize;

    private void Awake()
    {
        transform.localScale = _playerMaxSize;
    }
}
