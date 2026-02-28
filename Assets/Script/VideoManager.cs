using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        if (videoPlayer != null)
        {
            // 停止自動播放（確保手動控制）
            videoPlayer.playOnAwake = false;

            // 訂閱事件：影片結束後載入新場景
            videoPlayer.loopPointReached += OnVideoEnd;

            // 預載影片
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
        else
        {
            Debug.LogError("VideoPlayer not assigned in the Inspector!");
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video prepared, starting playback...");
        vp.Play();
    }

    private void Update()
    {
        // 若按下 K 鍵，中斷影片並切換場景
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Angry Animal Play");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 當影片播放結束時載入下一個場景
        SceneManager.LoadScene("Angry Animal Play");
    }
}
