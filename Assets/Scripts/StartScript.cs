using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 144;
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Game Started!\n");
    }
}
