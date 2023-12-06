using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        //144hz by default
        Application.targetFrameRate = 144;
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene(3);
    }
            

    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Game Started!\n");
    }

    public void GoToCreditScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
