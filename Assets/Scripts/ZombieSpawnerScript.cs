using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform target; // The target for the zombies to chase
    public AudioClip gotShotSound;
    public GameObject bloodObject; // the blood object to spawn when shot
    public GameObject deathBloodObject; // the blood object to spawn when dies
    public float spawnInterval = 3.0f; // The time interval between spawns

    private float timeSinceLastSpawn;

    //DOESNT WORK ANYMORE
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            // Spawn a new zombie
            GameObject zombie = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            
            // Get the ZombieAI component of the zombie
            ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
            
            zombieAI.SetTarget(target);
            zombieAI.gotShotSound = gotShotSound;
            zombieAI.bloodObject = bloodObject;
            zombieAI.deathBloodObject = deathBloodObject;

            timeSinceLastSpawn = 0f;
        }
    }
}
