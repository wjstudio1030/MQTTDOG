using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem Explosion;

    private bool isRigidbodyAdded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") )
        {
            // Check if the Rigidbody2D component is not already attached
            if (!isRigidbodyAdded)
            {
                // Play the Explosion ParticleSystem when a collision with a "building" occurs.
                Explosion.Play();
                

                // Delay the addition of Rigidbody2D by 1 second using Invoke.
                Invoke("AddRigidbody2D", 0.5f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            // Check if the Rigidbody2D component is not already attached
            if (!isRigidbodyAdded)
            {
                // Play the Explosion ParticleSystem when a collision with a "Hero" occurs.
                Explosion.Play();

                // Delay the addition of Rigidbody2D by 0.5 seconds using Invoke.
                Invoke("AddRigidbody2D", 0.5f);
            }
        }
    }

    private void AddRigidbody2D()
    {
        // Check again if the Rigidbody2D component is not already attached (double-check)
        if (!isRigidbodyAdded && !GetComponent<Rigidbody2D>())
        {
            // Add the Rigidbody2D component to the current game object
            Rigidbody2D rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            isRigidbodyAdded = true;

            // Additional settings for the Rigidbody2D can be adjusted here if needed
            // rigidbody2D.gravityScale = 1f;
            // rigidbody2D.mass = 1f;
            // etc.
        }
    }
}
