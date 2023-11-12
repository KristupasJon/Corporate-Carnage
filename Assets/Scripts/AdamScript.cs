using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamScript : MonoBehaviour
{

    // todo
    // make it so that the game deosnt crash when the player dies
    // make the player die when colliding with zombies
    // add sounds manager ig
    // add background music
    // improve zombie ai and player tracking around objects

    public float moveSpeed = 20f;
    public float sprintSpeed = 1000f;
    public int health = 50;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouseCursor();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        FireGun();
    }

    void FixedUpdate()
    {
        Move();
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
        if (Input.GetMouseButton(0) && animator.GetBool("Shoot") == false)
        {
            animator.SetBool("Shoot", true);

        }
        else
        {
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

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
