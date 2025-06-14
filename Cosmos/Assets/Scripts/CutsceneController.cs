using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string coopVideo = "cutscene_coop.mp4";
    public string greenSoloVideo = "cutscene_solo_green.mp4";
    public string blueSoloVideo = "cutscene_solo_blue.mp4";

    void Start()
    {
        string selectedVideo = GetVideoFileName();
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, selectedVideo);
        videoPlayer.url = path;

        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Prepare();
        StartCoroutine(PlayWhenReady());
    }

    System.Collections.IEnumerator PlayWhenReady()
    {
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("Level2");
    }

    string GetVideoFileName()
    {
        if (PlayerSelectionData.isCoop)
            return coopVideo;

        if (PlayerSelectionData.player1Index == 0)
            return greenSoloVideo;

        return blueSoloVideo;
    }
}