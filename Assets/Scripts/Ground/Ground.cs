using System.Collections;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _maxLength = 20f;
    [SerializeField] private float _maxWidth = 2f;
    [SerializeField] private float _groundReducingCoefficient = 1f;

    private float _groundMinSize = 0.2f;

    private Coroutine _groundReducingCoroutine;

    private void Awake()
    {
        transform.localScale = new Vector3(_maxWidth, 0, _maxLength);
    }

    private void OnEnable()
    {
        InputSystem.OnTouchDetection += ReduceGround;
    }

    private void OnDisable()
    {
        InputSystem.OnTouchDetection -= ReduceGround;
    }

    private void ReduceGround(bool isReduce)
    {
        if (isReduce)
        {
            _groundReducingCoroutine = StartCoroutine(ReduceGround());
        }
        else
        {
            StopCoroutine(_groundReducingCoroutine);
        }
    }

    private IEnumerator ReduceGround()
    {
        while (transform.localScale.x > _groundMinSize)
        {
            yield return new WaitForEndOfFrame();

            transform.localScale -= new Vector3(_groundReducingCoefficient * Time.deltaTime, 0, 0);
        }
    }
}
