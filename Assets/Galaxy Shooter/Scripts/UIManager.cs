using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject frozenScreen;
    public Text scoreText;
    public Text gameOver;
    public int score = 0;


    private void Start()
    {
        gameOver.enabled = false;
    }

    public void UpdateLives(int currentLives)
    {
      livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
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



}
