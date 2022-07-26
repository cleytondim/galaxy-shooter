using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private GameObject[] _powerups;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void RestartSpawns()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public IEnumerator SpawnEnemyRoutine()
    {
        while (!_gameManager.IsGameOver())
        {
            float randomX = Random.Range(-8, 9);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }

    public IEnumerator PowerupSpawnRoutine()
    {
        while (!_gameManager.IsGameOver())
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-8, 9), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }



}
