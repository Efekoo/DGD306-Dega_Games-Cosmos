using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;
using System.Net.NetworkInformation;

public class IntroCutScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string coopVideo = "Openingmp.mp4";
    public string greenSoloVideo = "Openinggreen.mp4";
    public string blueSoloVideo = "Openingblue.mp4";

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
        SceneManager.LoadScene("Tutorial");
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