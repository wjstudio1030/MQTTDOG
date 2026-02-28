using System.Collections;
using UnityEngine;


public class Projetile : MonoBehaviour
{
    [ReadOnly, SerializeField] private float _InitialVelocity = 5f;
    public float ShowVelocity
    {
        get => _InitialVelocity;
        set => _InitialVelocity = value;
    }

    [SerializeField] public float _Angle;
    [SerializeField] MqttDog mqttdog;
    [SerializeField] ChangeSprite changesprite;

    //[SerializeField] GameObject changup;

    public float timeSinceLastChange = 0f;
    public float changeInterval = 1f;
    public bool isIncreasing = true;
    public bool hasProjectileLaunched = false;
    public AudioClip audioclip1; // Assign this in the Inspector
    public AudioClip audioclip2; // Assign this in the Inspector
    public AudioClip audioclip3;
    public AudioClip audioclip4;
    public AudioClip audioclip5;
    public AudioClip audioclip6;
    public AudioSource audioSource;
    public bool CanShoot = false;
    public bool IsInvisible = false;
    public float angle;
    public float currentVelocity = 0.0f;
    public bool Candogvoice = false;
    public bool CanCatvoice = false;
    public bool CanBirdvoice = false;
    public bool CanHit = false;
    public GameObject raineffect;
    public GameObject stoneeffect;
    public GameObject cometupeffect;
    public GameObject cometdowneffect;
    public GameObject change;

    [SerializeField] public GameObject[] energyObjects;


    public void Update()
    {
        audioSource = GetComponent<AudioSource>();

       
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.I))
        {
            CanShoot = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !hasProjectileLaunched && CanShoot)
        {
            // Set the flag to true, indicating that the projectile has been launched
            hasProjectileLaunched = true;
            IsInvisible = true;
            CanShoot = false;
            CanHit = true;

            change.SetActive(false);

            // Launch the projectile
            angle = _Angle * Mathf.Deg2Rad;

            StopAllCoroutines();

            StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));

            // Start the cooldown before the space key can be pressed again
            StartCoroutine(CooldownSpaceKey());

            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            StartCoroutine(DelayedAppear(2.5f));


        
        }

        // Increment or decrement _InitialVelocity based on isIncreasing flag
        if (Input.GetKey(KeyCode.Space) && CanShoot && !changesprite.isLocked)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= changeInterval)
            {
                if (isIncreasing)
                {
                    _InitialVelocity += 1f;
                    if (_InitialVelocity >= 14f)
                    {
                        _InitialVelocity = 14f;
                        isIncreasing = false;
                    }
                }
                else
                {
                    _InitialVelocity -= 1f;
                    if (_InitialVelocity <= 5f)
                    {
                        _InitialVelocity = 5f;
                        isIncreasing = true;
                    }
                }
                timeSinceLastChange = 0f;
            }
            for (int i = 0; i < energyObjects.Length; i++)
            {
                if (_InitialVelocity >= i + 6)
                {
                    energyObjects[i].SetActive(true);
                }
            }
            for (int i = 0; i < energyObjects.Length; i++)
            {
                if (_InitialVelocity < i + 6)
                {
                    energyObjects[i].SetActive(false);
                }
            }
            change.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _InitialVelocity = 5f;
        }


        if (Input.GetKeyDown(KeyCode.P) && !changesprite.isLocked)
        {
            Candogvoice = true;
        }
        if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.I)) && !changesprite.isLocked)
        {
            Candogvoice = false;
        }
        if (Input.GetKeyDown(KeyCode.O) && !changesprite.isLocked)
        {
            CanCatvoice = true;
        }
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.I)) && !changesprite.isLocked)
        {
            CanCatvoice = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && !changesprite.isLocked)
        {
            CanBirdvoice = true;
        }

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O)) && !changesprite.isLocked)
        {
            CanBirdvoice = false;
        }

        if (Candogvoice && Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(PlayAudioClipsSequentially(audioclip1, audioclip4));
            raineffect.SetActive(true);
            Candogvoice = false;
        }

        if (CanCatvoice && Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(PlayAudioClipsSequentially(audioclip2, audioclip5));
            stoneeffect.SetActive(true);
            CanCatvoice = false;
        }

        if (CanBirdvoice && Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(PlayAudioClipsSequentially(audioclip3, audioclip6));
            cometupeffect.SetActive(true);
            cometdowneffect.SetActive(true);
            CanBirdvoice = false;
        }
    }



    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.Stop(); // Stop any currently playing audio
        audioSource.clip = clip;
        audioSource.Play();
    }

    public IEnumerator Coroutine_Movement(float v0, float angle)
    {
        float t = 0;
        while (t < 3f) // Projectile will move for 3 seconds only
        {
            float x = v0 * t * Mathf.Cos(angle) - 4.78f;
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2) - 1.42f;
            transform.position = new Vector2(x, y);
            t += Time.deltaTime;
            yield return null;
        }
        // Projectile has stopped moving, set the position to (0, 0, 0)
        transform.position = new Vector2(-4.88f, -1.44f);
        hasProjectileLaunched = false;
    }

    public IEnumerator CooldownSpaceKey()
    {
        changesprite.isLocked = true;
        yield return new WaitForSeconds(3f);
        // Reset the flag to allow the space key to be pressed again
        hasProjectileLaunched = false;
        changesprite.isLocked = false;
        //Candogvoice = false;
        //CanCatvoice = false;
        //CanBirdvoice = false;
    }

    public IEnumerator DelayedAppear(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified delay time

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Check if the sprite renderer is not null
        if (IsInvisible)
        {
            // Set the sprite renderer color to transparent
            sprite.color = new Color(0f, 0f, 0f, 0f); // R, G, B, Alpha (transparent)
        }

    }
    public IEnumerator PlayAudioClipsSequentially(params AudioClip[] clips)
    {
        foreach (AudioClip clip in clips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length); // Wait for the clip to finish playing
        }
    }

    



    /* void UpdateChangeEnergy()
     {
         for (int i = 5; i < changup.transform.childCount; i++)
         {
             if (_InitialVelocity > i)
             {
                 changup.transform.GetChild(i).gameObject.SetActive(true);
             }
             else
             {
                 gameObject.SetActive(false);
             }
         }
     }
     */


}
