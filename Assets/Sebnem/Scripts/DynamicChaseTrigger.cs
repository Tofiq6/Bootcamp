using UnityEngine;
using TMPro;
using System.Collections;

public class DynamicChaseTrigger : MonoBehaviour
{
    public Follower follower; // Takipçi karakteri
    public float followDuration = 10f; // Takip süresi (saniye cinsinden)
    public string[] subtitles; // Ekranda sýrasýyla gösterilecek yazýlar
    public AudioClip[] subtitleAudios; // Her bir alt yazý için ses dosyasý
    public TextMeshProUGUI subtitleText; // Ekrandaki alt yazý metni
    public DynamicTask dynamicTask;
    private bool isTriggered = false; // Trigger tetiklendi mi?
    private int currentSubtitleIndex = 0; // Þu anda oynatýlan yazýnýn indeksi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered) // Eðer oyuncu trigger alanýna girdiyse ve henüz tetiklenmediyse
        {
            dynamicTask.StartTask("görev 1", "görev 2");
            isTriggered = true; // Trigger tetiklendi
            follower.StartFollowing(); // Takip etmeye baþla
            StartCoroutine(FollowSequence()); // Takip süreci baþlasýn
            ShowSubtitles(); // Alt yazýlarý göster
        }
    }

    private void ShowSubtitles()
    {
        StartCoroutine(DisplaySubtitlesCoroutine());
    }

    private IEnumerator DisplaySubtitlesCoroutine()
    {
        int subtitleIndex = 0; // Alt yazýlar arasýnda geçiþ yapmak için
        foreach (var audioClip in subtitleAudios) // Her ses dosyasý için
        {
            int numOfSubtitles = subtitles.Length;
            float durationPerSubtitle = audioClip.length / numOfSubtitles; // Sesin süresini alt yazýlara bölelim

            // Bu ses dosyasýna ait alt yazýlarý sýrayla göster
            while (subtitleIndex < numOfSubtitles)
            {
                subtitleText.text = subtitles[subtitleIndex]; // Alt yazýyý göster
                PlaySubtitleAudio(audioClip); // Ses dosyasýný çal

                yield return new WaitForSeconds(durationPerSubtitle); // Ses süresini alt yazýlara böldük

                subtitleIndex++; // Bir sonraki alt yazýya geç
            }
        }

        // Alt yazý bittiðinde texti temizle
        subtitleText.text = "";
    }

    private void PlaySubtitleAudio(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position); // Ses dosyasýný çal
        }
    }

    private IEnumerator FollowSequence()
    {
        // Takip süresi boyunca oyuncuyu takip et
        yield return new WaitForSeconds(followDuration);
        follower.StopFollowing(); // Takip süresi bittiðinde takip durur
    }
}
