using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdamScript : MonoBehaviour
{

    // todo:
    // 
    // add background music

    public float moveSpeed = 200f;
    public float sprintSpeed = 250f;
    public int health = 100;
    public float maxStamina = 100;
    private float currentStamina = 0;
    public Text healthUI;
    public Text bulletUI;
    public Slider staminaUI;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool isCooldown = false; // Flag to check if invunerability is active
    private bool isTired = false; // Flag to check if stamina bar is empty
    public AudioClip gunShotSound;
    public AudioClip gettingPunchedSound;
    public AudioClip handgunReloadSound;
    public AudioClip pickingUpHealth;
    public AudioClip pickingUpAmmo;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public int bulletsInClip = 10;
    public int maxBulletsInClip = 10;
    public bool isReloading = false;
    public GameObject GameOverScreen;
    public GameObject PauseScreen;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        bulletsInClip = maxBulletsInClip;
        currentStamina = maxStamina;
        staminaUI.maxValue = maxStamina;
        staminaUI.value = maxStamina;

        InitUI();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            FaceMouseCursor();
            if (Input.GetButtonDown("Fire1"))
            {
                FireGun();
            }
            if (Input.GetKey(KeyCode.R) || bulletsInClip == 0)
            {
                Reload();
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseScreen.activeInHierarchy)
            {
                PauseGame();
            }
            else if (PauseScreen.activeInHierarchy)
            {
                ContinueGame();
            }
        }

    }

    void Gameover()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void FixedUpdate()
    {
        Move();
        UseStamina(1);
    }

    void InitUI()
    {
        GameOverScreen.SetActive(false);
        PauseScreen.SetActive(false);
        healthUI.text = health.ToString();
        bulletUI.text = bulletsInClip.ToString() + "/" + maxBulletsInClip.ToString();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void UseStamina(float amount)
    {
        if (currentStamina + amount >= 0 && currentStamina + amount <= maxStamina)
        {
            currentStamina += amount;
            staminaUI.value = currentStamina;
            Debug.Log(currentStamina);
        }

        if(currentStamina == maxStamina && isTired == true)
        {
            isTired = false;
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
        AudioSource.PlayClipAtPoint(handgunReloadSound, transform.position, 1);
        yield return new WaitForSeconds(2);
        bulletsInClip = maxBulletsInClip;
        bulletUI.text = bulletsInClip.ToString() + "/" + maxBulletsInClip.ToString();
        isReloading = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")) == true && !isCooldown)
        {
            AudioSource.PlayClipAtPoint(gettingPunchedSound, transform.position, 1);
            ChangeHealth(-10);
            if (health <= 0)
            {
                Debug.Log("Game Over Adam is dead!");
                Gameover();
            }
            if (health > 0)
            {
                StartCoroutine(DamageCooldown());
            }
        }

        if (collision.gameObject.CompareTag("HealthPower") == true)
        {
            AudioSource.PlayClipAtPoint(pickingUpHealth, transform.position, 1);
            ChangeHealth(25);
        }

        if (collision.gameObject.CompareTag("AmmoPower") == true)
        {
            AudioSource.PlayClipAtPoint(pickingUpAmmo, transform.position, 1);
            ChangeMaxAmmunition(5);
        }
    }

    IEnumerator DamageCooldown()
    {
        isCooldown = true; 
        float endTime = Time.time + 3.0f; 

        // Blinking effect
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true;
        isCooldown = false;
    }

    void ChangeHealth(int x)
    {
        health += x;
        healthUI.text = health.ToString();
    }
    void ChangeMaxAmmunition(int x)
    {
        maxBulletsInClip += x;
        bulletUI.text = bulletsInClip.ToString() + "/" + maxBulletsInClip.ToString();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0)
        {
            movement = new Vector2(moveX, moveY);
            animator.SetFloat("Speed", movement.magnitude);

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > 0 && isTired == false)
            {
                // If Shift is being held down, increase the speed
                rb.velocity = (movement * (moveSpeed + sprintSpeed)) * Time.deltaTime;
                UseStamina(-1.5f);
                isTired = (currentStamina <= 0);
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
            if (!isReloading && bulletsInClip != 0)
            {
                //the shoot animation doesnt work...
                animator.SetBool("Shoot", true);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(gunShotSound, transform.position, 1);
                bulletsInClip--;
                bulletUI.text = bulletsInClip.ToString() + "/" + maxBulletsInClip.ToString();
                animator.SetBool("Shoot", false);
            }
        }
        else
        {
            //animator.SetBool("Shoot", false);
        }
    }

    void FaceMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - transform.position;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        firePoint.transform.rotation = transform.rotation;

    }
}
