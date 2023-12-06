using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdamTutorial : MonoBehaviour
{
    public GameObject FirstMessage;
    public GameObject DamageMessage;
    public GameObject ReloadMessage;
    public GameObject HealthPowerMessage;
    public GameObject EndingScreen; // The ending screen you want to display when all enemies are dead
    public AdamScript Adam; // Assuming AdamScript is the name of the script attached to the Adam object
    private int adamhealth;
    private int adamClip;
    private bool damageMsg = false;
    private bool reloadMsg = false;
    private bool healthpower = false;

    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(EnableForSeconds(FirstMessage, 3));
        adamhealth = Adam.health;
        adamClip = Adam.bulletsInClip;
    }

    void Update()
    {
        if (Adam.health < adamhealth && damageMsg == false)
        {
            Time.timeScale = 0;
            StartCoroutine(EnableForSeconds(DamageMessage, 5));
            damageMsg = true;
        }

        if (Adam.health > adamhealth && healthpower == false)
        {
            Time.timeScale = 0;
            StartCoroutine(EnableForSeconds(HealthPowerMessage, 5));
            healthpower = true;
        }

        if (Adam.bulletsInClip == 0 && reloadMsg == false)
        {
            Time.timeScale = 0;
            StartCoroutine(EnableForSeconds(ReloadMessage, 5));
            reloadMsg = true;
        }

        // Check if all enemies are dead
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Time.timeScale = 0;
            StartCoroutine(ShowEndingScreenAndLoadScene(EndingScreen, 5, 0));
        }
    }

    IEnumerator EnableForSeconds(GameObject obj, float seconds)
    {
        obj.SetActive(true);
        yield return new WaitForSecondsRealtime(seconds);
        obj.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator ShowEndingScreenAndLoadScene(GameObject obj, float seconds, int sceneIndex)
    {
        obj.SetActive(true);
        yield return new WaitForSecondsRealtime(seconds);
        obj.SetActive(false);
        yield return new WaitForSecondsRealtime(5); // Wait an additional 5 seconds
        SceneManager.LoadScene(sceneIndex); // Load the new scene
    }

    public void RetryTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
        Debug.Log("Tutorial is reset");
    }
}
