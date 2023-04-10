using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private float _timeBeforeDestroy = 0.6f;

    private void OnEnable()
    {
        Destroy(gameObject, _timeBeforeDestroy);
    }
}
