using System.Collections;
using UnityEngine;
using TMPro;

public class AudioSubtitleTrigger : MonoBehaviour
{
    [System.Serializable]
    public class AudioSubtitlePair
    {
        public AudioClip audioClip;
        [TextArea]
        public string subtitleText;
    }

    public AudioSubtitlePair[] sequence;
    public TextMeshProUGUI subtitleTextUI;
    public AudioSource audioSource;
    public GameObject vfxObject;
    public GameObject finalGameObject;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlaySequence());
        }
    }

    private IEnumerator PlaySequence()
    {
        foreach (var pair in sequence)
        {
            subtitleTextUI.text = pair.subtitleText;
            audioSource.clip = pair.audioClip;
            audioSource.Play();
            yield return new WaitForSeconds(pair.audioClip.length);
        }

        subtitleTextUI.text = "";

        // VFX ve finalGameObject ayný anda aktif olsun
        vfxObject.SetActive(true);
        finalGameObject.SetActive(true);

        // 2 saniye sonra sadece VFX kapanacak
        yield return new WaitForSeconds(2f);
        vfxObject.SetActive(false);
    }

}
