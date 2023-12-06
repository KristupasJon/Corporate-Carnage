using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionScript : MonoBehaviour
{
    public GameObject WinScreen;

    private void Start()
    {
        WinScreen.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Adam") && GameWon())
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
        }
    }

    bool GameWon()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Debug.Log("All enemies are dead!");
            return true;
        }
        else
        {
            return false;
        }
    }
}
