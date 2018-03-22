using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
    private bool GameIsPaused = false;
    public GameObject MenuUI;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            if (GameIsPaused)
                Resume();
            else
                Pause();
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
    void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
