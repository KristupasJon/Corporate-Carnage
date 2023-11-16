using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private AIDestinationSetter AIdest;
    public AudioClip gotShotSound;
    public Transform target;
    public GameObject bloodObject; // the blood object to spawn when shot
    public GameObject deathBloodObject; // the blood object to spawn when dies
    public int health = 20;

    private void Start()
    {
        AIdest = GetComponent<AIDestinationSetter>();
    }

    //for the zombie spawner
    public void SetTarget(Transform target)
    {
        AIdest.target = target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            AudioSource.PlayClipAtPoint(gotShotSound, transform.position, 0.2f);
            Instantiate(bloodObject, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity); // spawn blood object

            if (health <= 0)
            {
                Instantiate(deathBloodObject, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity); // spawn death blood object
                Destroy(gameObject);
            }
        }
    }
}
