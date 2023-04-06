using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _maxSize = 0f;
    [SerializeField] private float _minSize = 0f;

    private void OnEnable()
    {
        InputSystem.OnTouchDetection += Print;
    }

    private void OnDisable()
    {
        InputSystem.OnTouchDetection -= Print;
    }

    public void Print(bool IsPressed)
    {
        Debug.Log(IsPressed + "!");
    }
}
