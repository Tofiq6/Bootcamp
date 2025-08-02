using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;
    public TMP_Text interactionText;
    public Boat boat;
    public AudioSource audioSource;        // Eklenen: Ses efekti için AudioSource
    public AudioClip dungeonEnterSound;    // Eklenen: Dungeon sahnesi sesi

    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);

        if (boat != null && GameManager.Instance.isLyraInBoat)
        {
            boat.LyraInTheBoat();
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            if (sceneToLoad == "DungeonScene")
            {
                StartCoroutine(PlaySoundThenLoadScene());
            }
            else
            {
                GameManager.Instance.SceneLoader(sceneToLoad);
            }
        }
    }

    private IEnumerator PlaySoundThenLoadScene()
    {
        if (audioSource != null && dungeonEnterSound != null)
        {
            audioSource.PlayOneShot(dungeonEnterSound);
        }

        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.SceneLoader(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (interactionText != null)
            {
                interactionText.text = "Press 'G'";
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);
        }
    }
}
