using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single Player Game Loading...");
        SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
    }

    public void LoadCoOpGame()
    {
        Debug.Log("Co-Op Game Loading...");
        SceneManager.LoadScene("Co-Op_Mode", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            //Debug.Break();
            EditorApplication.isPlaying = false;
        }
    }
}
