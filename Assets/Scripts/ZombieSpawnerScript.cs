using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform target; // The target for the zombies to chase
    public float spawnInterval = 3.0f; // The time interval between spawns

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            // Spawn a new zombie
            GameObject zombie = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            
            // Get the ZombieAI component of the zombie
            ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
            
            // Assign the target to the zombie
            zombieAI.target = target;

            timeSinceLastSpawn = 0f;
        }
    }
}
