using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAdamCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Adam") == true)
        {
            Destroy(gameObject);
        }
    }
}
