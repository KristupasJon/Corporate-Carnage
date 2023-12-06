using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogicScript : MonoBehaviour
{
    public GameObject PauseScreen;
    // Start is called before the first frame update
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
        Debug.Log("Game Unpaused, transitioning to the next level!");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        Debug.Log("Game Unpaused");
    }
    public void RetryGame()
    {
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Debug.Log("Game was reset by user");
    }

    public void GoToMenu()
    {
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Debug.Log("Player went to menu");
    }
}
