using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerReducingCoefficient = 0f;

    private Player _player;

    private Coroutine _coroutine;

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
            _coroutine = StartCoroutine(ReducePlayer());
        else
            StopCoroutine(_coroutine);
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
