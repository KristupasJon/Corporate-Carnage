using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunBulletScript : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        transform.position += (transform.up * speed) * Time.deltaTime;
        Destroy(gameObject, 3f);
    }
}
