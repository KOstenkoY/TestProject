using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _destructionPrefab = null;

    [SerializeField] private float _timeBeforeDie = 0.5f;

    public void KillEnemy(Color color)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = color;

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_timeBeforeDie);

        gameObject.SetActive(false);

        if (_destructionPrefab != null)
        {
            Instantiate(_destructionPrefab, transform.position, transform.rotation);
        }
    }
}
