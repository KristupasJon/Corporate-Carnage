using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfficeSecurityScript : MonoBehaviour
{
    public int health = 100;
    //public Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip gunShotSound;
    public AudioClip gettingPunchedSound;
    public AudioClip handgunReloadSound;
    private int bulletsInClip = 10;
    public int maxBulletsInClip = 10;
    public bool isReloading = false;

    public float shootingTime = 3f; // Shooting interval in seconds
    private float nextShotTime = 0f; // Time of the next allowed shot

    void Start()
    {
        bulletsInClip = maxBulletsInClip;
    }

    void Update()
    {
        nextShotTime -= Time.deltaTime; // Update the next allowed shot time by subtracting deltaTime
        FaceTowardsAdam();
        // Check if Adam is in clear line of sight
        GameObject adam = GameObject.FindWithTag("Adam");
        if (adam != null)
        {
            Vector3 direction = adam.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity);

            // Draw the raycast
            Debug.DrawLine(transform.position, adam.transform.position, Color.red);
            //FIX THIS LATER
            if (hit.collider != null /*&& hit.collider.gameObject == adam*/ && nextShotTime <= 0f)
            {
                FireGun1();
                nextShotTime = shootingTime; // Set the time of the next allowed shot
            }
        }
    }

    void FaceTowardsAdam()
    {
        // Get the GameObject tagged as "Adam"
        GameObject adam = GameObject.FindWithTag("Adam");

        // Check if the Adam object exists
        if (adam != null)
        {
            // Calculate the direction vector from the sprite to Adam
            Vector3 direction = adam.transform.position - transform.position;

            // Get the sprite renderer component
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            // Update the facing direction based on the direction vector
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }



    void Reload()
    {
        if (!isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        // Play reload sound
        AudioSource.PlayClipAtPoint(handgunReloadSound, transform.position, 1);
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);
        bulletsInClip = maxBulletsInClip;
        isReloading = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Add bleeding and sound effects
        if (collision.gameObject.CompareTag("Bullet") == true)
        {
            ChangeHealth(-10);
        }
    }

    void ChangeHealth(int x)
    {
        health += x;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void FireGun1()
    {
        if (!isReloading && bulletsInClip != 0)
        {
            // Instantiate bullet prefab at fire point
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Play gunshot sound
            AudioSource.PlayClipAtPoint(gunShotSound, transform.position, 1);
            // Reduce bullets in clip
            bulletsInClip--;

            // Reload if no bullets left
            if (bulletsInClip == 0)
            {
                Reload();
            }
        }
    }
}
