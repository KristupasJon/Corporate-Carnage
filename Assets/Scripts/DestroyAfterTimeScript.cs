using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeScript : MonoBehaviour
{
    public float lifetime = 20.0f; // the time after which the object will be destroyed

    void Update()
    {
        lifetime -= Time.deltaTime; // subtract the time since the last frame from the lifetime

        if (lifetime <= 0) // if the lifetime has run out...
        {
            Destroy(gameObject); // destroy the object
        }
    }
}
