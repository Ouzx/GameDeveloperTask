// Oz
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public static bool IsGamePaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused) Resume();
            else PauseGame();
        }

    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.transform.GetChild(0).gameObject.SetActive(false);
        IsGamePaused = false;
        Time.timeScale = 1f;
    }
    public void PauseGame(bool _open = true)
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        if (_open)
            pauseMenuUI.transform.GetChild(0).gameObject.SetActive(true);
        IsGamePaused = true;
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Resume();
        GameManager.Instance.SetGame();
    }

    public void LevelMenu()
    {
        Resume();
        // SceneManager.LoadScene("Main Menu");
    }
    public void Win()
    {
        PauseGame(false);
        pauseMenuUI.transform.GetChild(1).gameObject.SetActive(true);

    }
    public void Fail()
    {
        PauseGame(false);
        pauseMenuUI.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        Resume();
        GameManager.Instance.NextLevel();
    }
    public void ResetMenu()
    {
        Resume();
        foreach (Transform v in pauseMenuUI.transform) v.gameObject.SetActive(false);
    }
}
