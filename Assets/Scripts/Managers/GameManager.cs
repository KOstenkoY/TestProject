using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player = null;
    [SerializeField] private Transform _playerStartrPosition = null;

    [SerializeField] private EnemySpawner _enemySpawner = null;

    private GameObject _currentPlayer;

    [Header("UI")]
    [SerializeField] private GameObject _gameplayUI = null;
    [SerializeField] private GameObject _endGameUI = null;
    [SerializeField] private GameObject _pauseUI = null;

    private const string _gameplaySceneName = "SampleScene";

    private void Awake()
    {
        StartGame();
    }

    private void OnEnable()
    {
        Finish.OnFinishGame += EndGame;
        TouchDetection.OnSetPause += SetPause;
        TouchDetection.OnRestartGame += RestartGame;
        TouchDetection.OnExitGame += Exit;
    }

    private void OnDisable()
    {
        Finish.OnFinishGame -= EndGame;
        TouchDetection.OnSetPause -= SetPause;
        TouchDetection.OnRestartGame -= RestartGame;
        TouchDetection.OnExitGame -= Exit;
    }

    private void StartGame()
    {
        if (_endGameUI.activeSelf)
            _endGameUI.SetActive(false);

        if (_pauseUI.activeSelf)
            _pauseUI.SetActive(false);

        _gameplayUI.SetActive(true);

        Time.timeScale = 1.0f;

        _currentPlayer = Instantiate(_player.transform.gameObject, _playerStartrPosition.position, _playerStartrPosition.rotation);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(_gameplaySceneName);
        
        //Destroy(_currentPlayer);

        //_enemySpawner.Restart();

        //StartGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0.0f;

        if (_gameplayUI.activeSelf)
            _gameplayUI.SetActive(false);

        _endGameUI.SetActive(true);
    }

    private void SetPause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0.0f;

            if (_gameplayUI.activeSelf)
                _gameplayUI.SetActive(false);

            if (_endGameUI.activeSelf)
                _endGameUI.SetActive(false);

            _pauseUI.SetActive(true);
        }
        else
        {
            _pauseUI.SetActive(false);

            _gameplayUI.SetActive(true);

            Time.timeScale = 1.0f;
        }
    }

    private void Exit()
    {
        Application.Quit();
    }
}




