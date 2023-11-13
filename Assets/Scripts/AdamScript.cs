using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamScript : MonoBehaviour
{

    // todo:
    // shooting animation doesnt work =(
    // make it so that the game deosnt crash when the player dies
    // add background music
    // improve zombie ai and player tracking around objects

    public float moveSpeed = 20f;
    public float sprintSpeed = 1000f;
    public int health = 10;
    public bool alive = true;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool isCooldown = false; // Flag to check if invunerability is active
    public AudioClip gunShotSound;
    public AudioClip gettingPunchedSound;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("test!");
    }

    void Update()
    {
        FaceMouseCursor();
        if (Input.GetButtonDown("Fire1"))
        {
            FireGun();
        }
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //change this later, to either more descriptive tags or simply a sound manager
        if (collision.gameObject.CompareTag("Killable") == true && !isCooldown)
        {
            AudioSource.PlayClipAtPoint(gettingPunchedSound, transform.position, 1);
            ChangeHealth(-10);
            if (health <= 0)
            {
                //display a game over screen here
                Debug.Log("ADAM IS DEAD!");
                Destroy(gameObject);
                alive = false;
            }
            if(health > 0)
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        isCooldown = true; // Set cooldown flag to true
        float endTime = Time.time + 3.0f; // Set the end time for the cooldown

        // Blinking effect
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true; // Make sure the sprite is visible at the end of the cooldown
        isCooldown = false;
    }

    void ChangeHealth(int x)
    {
        health += x;
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0)
        {
            movement = new Vector2(moveX, moveY);
            animator.SetFloat("Speed", movement.magnitude);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // If Shift is being held down, increase the speed
                rb.velocity = (movement * (moveSpeed + sprintSpeed)) * Time.deltaTime;
            }
            else
            {
                rb.velocity = (movement * moveSpeed) * Time.deltaTime;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
            rb.velocity = Vector2.zero;
        }
    }

    void FireGun()
    {
        if (Input.GetMouseButton(0))
        {
            //the shoot animation doesnt work...
            animator.SetBool("Shoot", true);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioSource.PlayClipAtPoint(gunShotSound, transform.position, 1); // Play the gunshot sound
            animator.SetBool("Shoot", false);
        }
    }

    void FaceMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;
        //the character sprite is facing the wrong way so this is an awkward fix for that
        transform.Rotate(0, 0, 90);
    }
}
