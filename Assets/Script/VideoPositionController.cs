using UnityEngine;
using UnityEngine.Video;

public class VideoPositionController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Update()
    {
        // Check if the Space key is held down
        if (Input.GetKey(KeyCode.Space))
        {
            // Play the video if it's not already playing
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.enabled = true;
                videoPlayer.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Pause the video when the Space key is not held down
            videoPlayer.Pause();

            // Disable the VideoPlayer component
            videoPlayer.enabled = false;
        }

        // Check if the Space key is pressed again to enable the VideoPlayer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Enable the VideoPlayer component
            videoPlayer.enabled = true;
        }
    }
}
