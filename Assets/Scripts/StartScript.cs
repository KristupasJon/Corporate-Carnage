using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void GoToGameScene()
    {
        Application.targetFrameRate = 60;
        SceneManager.LoadScene(1);
        Debug.Log("Game Started!\n");
    }
}
