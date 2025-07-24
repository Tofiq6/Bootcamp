using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;         // Inspector'dan sahne adý atanacak
    public TMP_Text interactionText;   // "Press 'G'" yazacak TextMeshPro UI objesi
    public Boat boat;
    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);

        if(boat != null && GameManager.Instance.isLyraInBoat)
        {
            boat.LyraInTheBoat();
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.SceneLoader(sceneToLoad);
        }
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