using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        switch (levelManager.selectedLevel)
        {
            case "level 1":
                SceneManager.LoadScene("FirstLevel");
                break;
            case "level 2":
                SceneManager.LoadScene("SecondLevel");
                break;
            case "level 3":
                SceneManager.LoadScene("ThirdLevel");
                break;
        }
       
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}

