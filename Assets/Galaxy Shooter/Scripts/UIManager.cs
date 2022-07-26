using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject frozenScreen;
    public GameObject pauseScreen;
    public Text scoreText;
    public Text gameOver;
    public Text bestScoreText;
    public int score = 0;
    public bool pause = false;

    private void Start()
    {
        gameOver.enabled = false;
    }


    public void UpdateLives(int currentLives)
    {
        if(currentLives>=0)
        {
            livesImageDisplay.sprite = lives[currentLives];
        }
      
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void SetBestScore(int bestScore)
    {
        bestScoreText.text = "Best: " + bestScore;
    }

    public void NewGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
   
    public void ShowTitleScreen()
    {
        frozenScreen.SetActive(true);
        gameOver.enabled = true;
    }

    public void HideTitleScreen()
    {
        NewGame();
        frozenScreen.SetActive(false);
        gameOver.enabled = false;
    }

    public void SetPause(bool pause)
    {
        pauseScreen.SetActive(pause);
        
    }



}
