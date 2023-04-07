using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerReducingCoefficient = 0.1f;

    private Player _player;

    private Coroutine _playerReduceCoroutine;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        TouchDetection.OnTouchDetection += ReducePlayer;
    }

    private void OnDisable()
    {
        TouchDetection.OnTouchDetection -= ReducePlayer;
    }

    private void ReducePlayer(bool isReduce)
    {
        if (isReduce)
        {
            _playerReduceCoroutine = StartCoroutine(ReducePlayer());
        }
        else
        {
            StopCoroutine(_playerReduceCoroutine);
        }
    }

    private IEnumerator ReducePlayer()
    {
        // compare the size of the player by x, because our player is a sphere
        while (transform.localScale.x > _player.PlayerMinSize.x)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale -= Vector3.one * (_playerReducingCoefficient * Time.deltaTime);
        }

        KillPlayer();
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }
}
