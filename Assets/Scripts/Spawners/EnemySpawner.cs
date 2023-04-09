using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab = null;
    [SerializeField] private Transform _enemyContainer = null;

    [Header("Spawn ettings")]
    // range where enemies spawn
    [SerializeField] private float _spawnLength = 12f;
    [SerializeField] private float _spawnWidth = 4f;

    [SerializeField] private int _countEnemies = 7;

    private int _amountToPool = 30;

    private List<GameObject> _enemiesList = new List<GameObject>();

    private void Awake()
    {
        InitializePool();

        RandomSpawnEnemies();
    }

    private void InitializePool()
    {
        GameObject tmp;

        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_enemyPrefab.gameObject);
            tmp.SetActive(false);

            tmp.transform.SetParent(_enemyContainer);

            _enemiesList.Add(tmp);
        }
    }

    private GameObject GetEnemies()
    {
        if(_enemiesList.Count != 0)
        {
            for(int i = 0; i < _enemiesList.Count; i++)
            {
                if (!_enemiesList[i].activeSelf)
                {
                    _enemiesList[i].SetActive(true);

                    return _enemiesList[i];
                }
            }
        }

        return null;
    }

    private void RandomSpawnEnemies()
    {
        for (int i = 0; i < _countEnemies; i++)
        {
            Vector3 position = Vector3.zero;

            position = new Vector3(
                    Random.Range(-_spawnWidth / 2, _spawnWidth / 2),
                    0,
                    Random.Range(-_spawnLength / 2, _spawnLength / 2));

            GameObject enemy = GetEnemies();

            enemy.transform.position = position;

            enemy.SetActive(true);
        }
    }

    public void Restart()
    {
        ClearEnemies();

        RandomSpawnEnemies();
    }

    private void ClearEnemies()
    {
        foreach(GameObject enemy in _enemyContainer)
        {
            enemy.SetActive(false);
        }
    }
}
