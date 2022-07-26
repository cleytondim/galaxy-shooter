using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameOver = true;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _coopPlayersPrefab;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    [SerializeField]
    private SpawnManager _spawnManager;

    public bool isCoopMode = false;

    public bool pause = false;

    private Animator _animator;

    public int bestScore = 0;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _animator = _pauseMenuPanel.GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        bestScore = PlayerPrefs.GetInt("BestScore");
        _uiManager.SetBestScore(bestScore);
        if (SceneManager.GetActiveScene().name != "Single_Player")
        {
            isCoopMode = true;
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isCoopMode)
                {
                    Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    Instantiate(_coopPlayersPrefab, Vector3.zero, Quaternion.identity);
                }
                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.RestartSpawns();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
            }
        }
        else if (!pause)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ManagePause();
            }
        }
    }

    public void ManagePause()
    {
        pause = !pause;
        _uiManager.SetPause(pause);
        _animator.SetBool("isPaused", pause);
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        int score = _uiManager.score;
        if(score > bestScore)
        {
            bestScore = score;
            _uiManager.SetBestScore(bestScore);
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        _uiManager.ShowTitleScreen();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void BackMainMenu()
    {
        ManagePause();
        Debug.Log("Main Menu Loading...");
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }
}
