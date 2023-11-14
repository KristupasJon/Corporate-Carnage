using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Transform target; // the object to chase
    public AudioClip gotShotSound;
    public GameObject bloodObject; // the blood object to spawn when shot
    public GameObject deathBloodObject; // the blood object to spawn when dies
    public float speed = 2.0f; // speed of the zombie
    public int health = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            AudioSource.PlayClipAtPoint(gotShotSound, transform.position, 0.2f);
            Instantiate(bloodObject, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity); // spawn blood object
            speed *= 0.5f;

            if (health <= 0)
            {
                Instantiate(deathBloodObject, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity); // spawn death blood object
                Destroy(gameObject);
            }
        }
    }

    void Move()
    {
        // calculate the direction towards the target
        Vector3 direction = target.position - transform.position;

        // normalize the direction (to get a direction vector of length 1)
        direction.Normalize();

        // move the zombie towards the target
        transform.position += (direction * speed) * Time.deltaTime;

        // calculate the angle towards the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // add 90 degrees to the angle as a correction for the sprite
        angle -= 90;

        // rotate the zombie to face the target
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        Move();
    }
}
