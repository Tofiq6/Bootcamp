using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;         // Inspector'dan sahne adý atanacak
    public TMP_Text interactionText;   // "Press 'G'" yazacak TextMeshPro UI objesi

    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false); // Baþta yazý gizli
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(sceneToLoad);
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
