using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerReducingCoefficient = 0.1f;

    [SerializeField] private float _jumpPower = 20f;

    [SerializeField] private float _maxDistanceToEnemy = 3;

    private Player _player;
    private PlayerWeapon _playerWeapon;

    private float _timeBeforeDetectEnemy = 0.7f;

    private Coroutine _playerReduceCoroutine;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerWeapon = GetComponent<PlayerWeapon>();
    }

    private void OnEnable()
    {
        TouchDetection.OnTouchDetection += ReducePlayer;
        Bullet.OnBulletHit += BulletHit;
    }

    private void OnDisable()
    {
        TouchDetection.OnTouchDetection -= ReducePlayer;
        Bullet.OnBulletHit -= BulletHit;
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

    private void BulletHit()
    {
        StartCoroutine(CheckObstacle());
    }

    // method returns the distance the player can move
    private IEnumerator CheckObstacle()
    {
        yield return new WaitForSeconds(_timeBeforeDetectEnemy);

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        
        if (Physics.SphereCast(ray, transform.position.x / 2, out hit))
        {
            if (hit.transform.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                if (hit.distance > _maxDistanceToEnemy)
                {
                    Move(hit.distance - _maxDistanceToEnemy);
                }
                else
                {
                    Move(0);
                }
            }
            else
            {
                Move(hit.distance++);
            }
        }
        else
        {
            throw new System.Exception("Ray hasn't work");
        }


    }

    public void Move(float distance)
    {
        int countJumps;

        if (distance == 0)
            countJumps = 0;
        else if (distance < 1)
            countJumps = 1;
        else
            countJumps = (int)distance;

        transform.DOJump((transform.position + new Vector3(0, 0, distance)), _jumpPower, countJumps, countJumps * 0.5f, false);

        _playerWeapon.IsShoot = true;
    }
}
