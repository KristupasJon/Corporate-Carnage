using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunBulletScript : MonoBehaviour
{
    public float speed = 20f;
    public float dissapearTimer = 2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Indestruct"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position += (transform.right * speed) * Time.deltaTime;
        Destroy(gameObject, dissapearTimer);
    }
}
