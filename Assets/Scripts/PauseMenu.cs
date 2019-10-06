using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject UI;
    public Player player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.lockRotate = false;
    }

    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player.lockRotate = true;
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitingGame()
    {
        Application.Quit();
    }
}
