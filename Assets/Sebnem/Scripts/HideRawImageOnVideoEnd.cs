using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneLoader : MonoBehaviour
{
    public Button startButton;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public string sceneToLoad = "SampleScene";

    void Start()
    {
        rawImage.gameObject.SetActive(false);         // Baþlangýçta gizli
        videoPlayer.gameObject.SetActive(false);      // VideoPlayer da gizli
        startButton.onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        startButton.gameObject.SetActive(false);      // Butonu gizle
        rawImage.gameObject.SetActive(true);          // Görüntüyü aç
        videoPlayer.gameObject.SetActive(true);       // VideoPlayer aktif
        videoPlayer.Play();                           // Video oynasýn
        videoPlayer.loopPointReached += OnVideoFinished; // Video bitince çaðýr
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);          // Sahne geçiþi
    }
}
