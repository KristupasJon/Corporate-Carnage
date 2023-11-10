using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Transform target; // the object to chase
    public float speed = 2.0f; // speed of the zombie
    void Start()
    {
        
    }
    

    void Update()
    {
        // calculate the direction towards the target
        Vector3 direction = target.position - transform.position;

        // normalize the direction (to get a direction vector of length 1)
        direction.Normalize();

        // move the zombie towards the target
        transform.position += direction * speed * Time.deltaTime;

        // calculate the angle towards the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // add 90 degrees to the angle as a correction
        angle -= 90;

        // rotate the zombie to face the target
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
