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

    public bool _isMove = false;

    private Rigidbody _rigidbody = null;

    private Coroutine _bulletIncreaseCoroutine;

    private Color _color = Color.white;

    public static event System.Action OnBulletHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _color = gameObject.GetComponentInChildren<Renderer>().material.color;
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;

        transform.localScale = _bulletSize;

        _bulletIncreaseCoroutine = StartCoroutine(IncreaseBullet());
    }

    public void Shoot(Vector3 bulletMaxSize)
    {
        _bulletMaxSize = bulletMaxSize;

        StopCoroutine(_bulletIncreaseCoroutine);

        _rigidbody.velocity = Vector3.forward * _bulletSpeed;
    }

    private IEnumerator IncreaseBullet()
    {
        while (transform.localScale.x < _bulletMaxSize.x)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale += Vector3.one * (_bulletIncreaseCoefficient * Time.deltaTime);
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 2);

        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                enemyComponent.KillEnemy(_color);
            }
        }

        gameObject.SetActive(false);

        OnBulletHit?.Invoke();
    }
}
