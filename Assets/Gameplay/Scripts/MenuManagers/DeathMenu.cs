using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuUI;
    public LevelManager levelManager;

    public void DeathMenuActivate()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
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
