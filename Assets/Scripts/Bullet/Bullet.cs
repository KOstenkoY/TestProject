using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 3f;

    // must be equal to the same value as _playerReducingCoefficient in PlayerController.cs
    [SerializeField] private float _bulletIncreaseCoefficient = 0.1f;

    [SerializeField] private Vector3 _bulletSize = new Vector3(0.2f, 0.2f, 0.2f);

    [SerializeField] private Vector3 _bulletMaxSize = new Vector3(1.5f, 1.5f, 1.5f);

    private bool _isMove = false;

    private Rigidbody _rigidbody = null;

    private Coroutine _bulletIncreaseCoroutine;

    //private const string _enemyTag = "Enemy";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.localScale = _bulletSize;

        _bulletIncreaseCoroutine = StartCoroutine(IncreaseBullet());
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            _rigidbody.velocity = Vector3.forward * _bulletSpeed;
        }
    }

    public void Shoot(Vector3 bulletMaxSize)
    {
        _bulletMaxSize = bulletMaxSize;

        StopCoroutine(_bulletIncreaseCoroutine);

        _isMove = true;
    }

    private IEnumerator IncreaseBullet()
    {
        while (transform.localScale.x < _bulletMaxSize.x)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale += Vector3.one * (_bulletIncreaseCoefficient * Time.deltaTime);
        }

        //gameObject.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (TryGetComponent(_enemyTag))
    //    {
    //        other.GetComponent
    //    }
    //}
}
