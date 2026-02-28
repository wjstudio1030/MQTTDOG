using UnityEngine;
using UnityEngine.Video;

public class VideoReversal : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool isReversed = false;
    private float originalPlaybackSpeed;

    private void Start()
    {
        originalPlaybackSpeed = videoPlayer.playbackSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Change this to the key you want to use for reversal
        {
            isReversed = !isReversed;
            if (isReversed)
            {
                videoPlayer.playbackSpeed = -originalPlaybackSpeed;
            }
            else
            {
                videoPlayer.playbackSpeed = originalPlaybackSpeed;
            }
        }
    }
}
