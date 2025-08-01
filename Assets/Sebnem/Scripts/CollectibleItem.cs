using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public float interactRange = 3f;
    public AudioClip collectSound;
    public AudioSource audioSource; // Eklenen: Ses için AudioSource

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (distance <= interactRange)
        {
            if (interactText != null)
            {
                interactText.text = "Press E to Collect";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectItem();
            }
        }
        else
        {
            if (interactText != null)
            {
                interactText.text = "";
            }
        }
    }

    private void CollectItem()
    {
        GameManager.Instance.haveKey = true;
        GameManager.Instance.SaveGame();
        Debug.Log("Item Collected!");

        // Ses çal — artýk sahnedeki AudioSource kullanýlýyor
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        Destroy(gameObject);
    }
}
