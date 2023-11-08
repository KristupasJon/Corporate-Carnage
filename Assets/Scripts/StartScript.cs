using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    public void GoToGameScene()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        SceneManager.LoadScene(1);
        Debug.Log("Game Started!\n");
    }
}
