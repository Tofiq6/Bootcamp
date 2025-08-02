using UnityEngine;
using TMPro;
using System.Collections;

public class DynamicChaseTrigger : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleAudioPair
    {
        public string subtitle;
        public AudioClip audioClip;
    }

    public Follower follower; // Takipçi karakteri
    public float followDuration = 10f; // Takip süresi
    public SubtitleAudioPair[] subtitleSequence; // Altyazý + ses çiftleri
    public TextMeshProUGUI subtitleText; // Altyazý UI
    public AudioSource audioSource; // Ses kaynaðý (Inspector’dan atanmalý)
    public GameObject portal;
    private bool isTriggered = false;
    public bool Elandor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            FollowAndTalk();
        }
    }

    public void FollowAndTalk()
    {
        isTriggered = true;
        follower.StartFollowing();
        StartCoroutine(FollowSequence());
        ShowSubtitles();
    }

    private void ShowSubtitles()
    {
        StartCoroutine(DisplaySubtitlesCoroutine());
    }

    private IEnumerator DisplaySubtitlesCoroutine()
    {
        foreach (var pair in subtitleSequence)
        {
            subtitleText.text = pair.subtitle;

            if (pair.audioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(pair.audioClip);
                yield return new WaitForSeconds(pair.audioClip.length);
            }
            else
            {
                yield return new WaitForSeconds(2f); // Ses yoksa varsayýlan süre
            }
        }

        subtitleText.text = "";
    }

    private IEnumerator FollowSequence()
    {
        yield return new WaitForSeconds(followDuration);
        follower.StopFollowing();

        if (Elandor)
        {
            DynamicTask.Instance.StartTask("MIGHTY WISE", "Talk to Elandor");
        }

        if(portal != null)
        {
            portal.SetActive(true);
        }
    }
}
