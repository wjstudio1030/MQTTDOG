using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLife : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    [SerializeField] int CurrentHealth;
    [SerializeField] Projetile projetile;
    public HealthBar healthBar;
    public ParticleSystem damageParticles;

    private bool canTakeDamage = true;
    private bool IsInvisible = false;
    private bool shouldRotate = false;

    void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        
    }
    void Update()
    {
        if (shouldRotate)
        {
            // Rotate the object gradually over time
            float rotationSpeed = 20f;
            float targetZRotation = -4.689f;
            float step = rotationSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            // Check if the rotation is close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
            {
                // Delay for 5 seconds before setting shouldRotate to false
                StartCoroutine(DelayCoroutine(5f));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("building"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -4.689f);
            Debug.Log("rotation");
        }*/
        if (collision.gameObject.CompareTag("building"))
        {
            shouldRotate = true;
            Debug.Log("Collision with building");
        }

        if (collision.gameObject.CompareTag("Hero") && canTakeDamage && projetile.CanHit)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            // Check if the sprite renderer is not null
           StartCoroutine(DelayedTransparentSprite(2f));
            
            TakeDamage(34);

            // Start the coroutine to wait for 5 seconds before taking damage again
            StartCoroutine(DamageCooldown(5f));

            // Start the coroutine to activate particles after 2 seconds
            StartCoroutine(ActivateParticles(2f));

            StartCoroutine(TransformPosition(3f));

            StartCoroutine(ActivateParticles(4f));

            StartCoroutine(DelayedAppear(4.5f));   

            projetile.CanHit = false;
        }
        if (CurrentHealth <= 0)
        {
            // Start the coroutine to load the "Win" scene after 1 second
            StartCoroutine(LoadWinSceneDelayed(2.5f));
        }
    }
    private IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        shouldRotate = false;
    }


    private IEnumerator DelayedTransparentSprite(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified delay time

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Check if the sprite renderer is not null
        if (sprite != null)
        {
            // Set the sprite renderer color to transparent
            sprite.color = new Color(1f, 1f, 1f, 0f); // R, G, B, Alpha (transparent)
            IsInvisible = true;
        }
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
    }

    private IEnumerator DamageCooldown(float cooldownTime)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(cooldownTime);
        canTakeDamage = true;
    }

    private IEnumerator ActivateParticles(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // Activate the particle system
        if (damageParticles != null)
        {
            damageParticles.Play();
        }
    }
    private IEnumerator TransformPosition(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        float randomX = Random.Range(0f, 5f);

        transform.position = new Vector3(randomX, 7f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -4.689f);
    }
    private IEnumerator DelayedAppear(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified delay time

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Check if the sprite renderer is not null
        if (IsInvisible)
        {
            // Set the sprite renderer color to transparent
            sprite.color = new Color(255f, 255f, 255f, 255f); // R, G, B, Alpha (transparent)
        }

    }
    private IEnumerator LoadWinSceneDelayed(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the "Win" scene
        SceneManager.LoadScene("Win");
    }
}
