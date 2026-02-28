using UnityEngine;

public class StoneVoice : MonoBehaviour
{
    public AudioClip collisionSound; // Assign the audio clip in the Inspector

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool hasPlayed = false; // Flag to track whether the audio has been played

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the collision sound to the AudioSource
        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasPlayed && collision.gameObject.CompareTag("building"))
        {
            // Play the assigned audio clip from the AudioSource
            audioSource.Play();
            hasPlayed = true; // Set the flag to true
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayed && other.CompareTag("building"))
        {
            // Play the assigned audio clip from the AudioSource
            audioSource.Play();
            hasPlayed = true; // Set the flag to true
        }
    }

}
