using UnityEngine;
using TMPro;
using System.Collections;

public class DynamicChaseTrigger : MonoBehaviour
{
    public Follower follower; // Takipçi karakteri
    public float followDuration = 10f; // Takip süresi (saniye cinsinden)
    public string[] subtitles; // Ekranda sýrasýyla gösterilecek yazýlar
    public float[] subtitleDelay; // Yazýlar arasýndaki bekleme süresi (saniye cinsinden)
    public AudioClip followAudio; // Takip esnasýnda çalacak ses dosyasý
    public TextMeshProUGUI subtitleText; // Ekrandaki alt yazý metni

    private bool isTriggered = false; // Trigger tetiklendi mi?
    private int currentSubtitleIndex = 0; // Þu anda oynatýlan yazýnýn indeksi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered) // Eðer oyuncu trigger alanýna girdiyse ve henüz tetiklenmediyse
        {
            isTriggered = true; // Trigger tetiklendi
            follower.StartFollowing(); // Takip etmeye baþla
            StartCoroutine(FollowSequence()); // Takip süreci baþlasýn
            PlayAudio(); // Ses dosyasýný çal
            ShowSubtitles(); // Alt yazýlarý göster
        }
    }

    private void PlayAudio()
    {
        if (followAudio != null)
        {
            AudioSource.PlayClipAtPoint(followAudio, transform.position); // Ses dosyasýný çal
        }
    }

    private void ShowSubtitles()
    {
        StartCoroutine(DisplaySubtitlesCoroutine());
    }

    private IEnumerator DisplaySubtitlesCoroutine()
    {
        while (currentSubtitleIndex < subtitles.Length)
        {
            subtitleText.text = subtitles[currentSubtitleIndex]; // Þu anki yazýyý göster
            yield return new WaitForSeconds(subtitleDelay[currentSubtitleIndex]); // Yazý süresi kadar bekle
            currentSubtitleIndex++; // Bir sonraki yazýya geç
        }

        // Alt yazý bittiðinde texti temizle
        subtitleText.text = "";
    }

    private IEnumerator FollowSequence()
    {
        // Takip süresi boyunca oyuncuyu takip et
        yield return new WaitForSeconds(followDuration);
        follower.StopFollowing(); // Takip süresi bittiðinde takip durur
    }
}
