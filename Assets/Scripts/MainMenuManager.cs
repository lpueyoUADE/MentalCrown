using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void GameOverMenu()
    {
        SceneManager.LoadScene("GameOverMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
