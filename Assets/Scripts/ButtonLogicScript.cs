using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogicScript : MonoBehaviour
{
    public GameObject PauseScreen;
    //this function exists because I am a failure and made the scenes in a bad order. The very
    //first gameplay scene is made before the trivial other scenes in the menu.
    //too lazy to refactor everything
    public void NextLevel_first()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
        Debug.Log("Game Unpaused, transitioning to the next level!");
    }

    public void NextLevel_continious()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        SceneManager.LoadScene(buildIndex + 1);
        Debug.Log("Game Unpaused, transitioning to the next level!");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        Debug.Log("Game Unpaused.");
    }
    public void RetryGame()
    {
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Debug.Log("Game was reset by user.");
    }

    public void GoToMenu()
    {
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Debug.Log("Player went to menu.");
    }
}
