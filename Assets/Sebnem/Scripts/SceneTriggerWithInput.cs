using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using StarterAssets;

public class SceneTriggerWithInput : MonoBehaviour
{
    public string sceneToLoad;         // Inspector'dan sahne adý atanacak
    public TMP_Text interactionText;   // "Press 'G'" yazacak TextMeshPro UI objesi

    public ThirdPersonController tpc;
    private bool isPlayerInTrigger = false;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        tpc = GameObject.Find("PlayerArmature").GetComponent<ThirdPersonController>();
    }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("AAA"));

        if (PlayerPrefs.GetInt("AAA") == 2)
        {
            tpc._animator.SetBool("isCarry", true);
        }
        else 
        { 
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
