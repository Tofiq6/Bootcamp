using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;             // TextMeshPro için
using UnityEngine.Audio;  // Ses için
using UnityEngine.UI;

public class SubtitleTrigger : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public string text;              // Altyazý metni
        public float displayDuration;    // Altyazýnýn ekranda kalma süresi
        public float delayBeforeNext;    // Sonrakine geçmeden önce bekleme süresi
        public AudioClip audioClip;      // Çalýnacak ses dosyasý
    }

    public List<SubtitleLine> subtitleLines = new List<SubtitleLine>(); // Inspector’dan ayarlanacak
    public TMP_Text subtitleText;          // TextMeshPro UI objesi
    public AudioSource audioSource;        // Ses çalacak AudioSource
    public bool playOnce = true;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!hasPlayed || !playOnce))
        {
            hasPlayed = true;
            StartCoroutine(PlaySubtitles());
        }
    }

    IEnumerator PlaySubtitles()
    {
        foreach (var line in subtitleLines)
        {
            subtitleText.text = line.text;

            if (line.audioClip != null)
            {
                audioSource.clip = line.audioClip;
                audioSource.Play();
            }

            yield return new WaitForSeconds(line.displayDuration);

            subtitleText.text = "";

            yield return new WaitForSeconds(line.delayBeforeNext);
        }
    }
}
