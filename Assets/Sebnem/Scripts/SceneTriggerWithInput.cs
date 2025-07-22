using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using StarterAssets;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;         // Inspector'dan sahne adý atanacak
    public TMP_Text interactionText;   // "Press 'G'" yazacak TextMeshPro UI objesi

    private ThirdPersonController tpc;
    private bool isPlayerInTrigger = false;

    void Start()
    {

        if (sceneToLoad == "SampleScene" && PlayerPrefs.GetInt("AAA") == 1)
        {
            tpc._animator.SetBool("isCarry", true);
        }
        else
        {
            PlayerPrefs.SetInt("AAA", 0);
            tpc._animator.SetBool("isCarry", false);
        }

        if (interactionText != null)
            interactionText.gameObject.SetActive(false); // Baþta yazý gizli
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt("AAA", PlayerPrefs.GetInt("AAA") + 1);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpc = other.gameObject.GetComponent<ThirdPersonController>();

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
