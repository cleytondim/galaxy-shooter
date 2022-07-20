using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver = true;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private SpawnManager _spawnManager;


    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.RestartSpawns();
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        _uiManager.ShowTitleScreen();
    }

    public bool isGameOver()
    {
        return gameOver;
    }
}
