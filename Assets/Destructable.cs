using UnityEngine;

public class Destructable : MonoBehaviour
{
    private int hitCount = 0;
    public AudioClip destroyedSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;

            if (hitCount >= 10)
            {
                AudioSource.PlayClipAtPoint(destroyedSound, transform.position, 1f);
                Destroy(gameObject);
            }
        }
    }
}
