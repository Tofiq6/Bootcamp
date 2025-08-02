using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;
    public TMP_Text interactionText;
    public Boat boat;
    public AudioSource audioSource;
    public AudioClip dungeonEnterSound;
    public LoadingScreenManager loadingScreenManager;
    public GameObject Kael;
    public GameObject TavernQuit;

    public bool Taverna = false;
    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);

        if (boat != null && GameManager.Instance.isLyraInBoat)
        {
            boat.LyraInTheBoat();
        }

        if (Taverna && GameManager.Instance.isTavernQuit)
        {
            Kael.transform.position = TavernQuit.transform.position;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            if (Taverna)
            {
                GameManager.Instance.isTavernQuit = true;
                GameManager.Instance.SaveGame();
            }

            if (sceneToLoad == "DungeonScene")
            {
                
                StartCoroutine(PlaySoundThenLoadScene());
            }
            else
            {

                loadingScreenManager.LoadScene(sceneToLoad);
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

        loadingScreenManager.LoadScene(sceneToLoad);        
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
