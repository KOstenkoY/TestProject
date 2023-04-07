using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _bulletPrefab = null;
    [SerializeField] private Transform _bulletSpawnPosition = null;

    [SerializeField] private int _amountToPool = 1;

    private GameObject _currentBullet = null;

    private List<GameObject> _bulletsList = new List<GameObject>();


    private void Awake()
    {
        InitializePool();
    }

    private void OnEnable()
    {
        InputSystem.OnTouchDetection += Shoot;
    }

    private void OnDisable()
    {
        InputSystem.OnTouchDetection -= Shoot;
    }

    private void InitializePool()
    {
        GameObject tmp;

        for(int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_bulletPrefab.gameObject);
            tmp.SetActive(false);

            _bulletsList.Add(tmp);
        }
    }

    public void Shoot(bool isShoot)
    {
        if (isShoot)
        {
            _currentBullet = GetBullet();

            if (_currentBullet != null)
            {
                _currentBullet.transform.position = _bulletSpawnPosition.position;
                _currentBullet.transform.rotation = _bulletSpawnPosition.rotation;

                _currentBullet.SetActive(true);
            }
            else
            {
                throw new System.Exception("ObjectPool hasn't any objects. Check PlayerWeapon script.");
            }
        }
        else
        {
            if (_currentBullet.TryGetComponent<Bullet>(out Bullet bulletComponent))
            {
                bulletComponent.Shoot(transform.localScale);
            }
            else
            {
                throw new System.Exception("Bullet hasn't Bullet script. Check bullet prefab.");
            }
        }
    }

    private GameObject GetBullet()
    {
        if(_bulletsList.Count != 0)
        {
            for(int i = 0; i < _bulletsList.Count; i++)
            {
                if (!_bulletsList[i].activeSelf)
                {
                    _bulletsList[i].SetActive(true);

                    return _bulletsList[i];
                }
            }
        }

        return null;
    }
}
