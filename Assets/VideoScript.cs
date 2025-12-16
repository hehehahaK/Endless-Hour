using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    public VideoPlayer videoPlayer; // assign your VideoPlayer component
    public int sceneIndex; // scene to load after video

    private void Start()
    {
        if (videoPlayer != null)
        {
            // Subscribe to loopPointReached event, triggers when video ends
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play(); // start video
        }
        else
        {
            Debug.LogWarning("VideoPlayer not assigned!");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Switch scene when video finishes
        SceneManager.LoadScene(sceneIndex);
    }
}
