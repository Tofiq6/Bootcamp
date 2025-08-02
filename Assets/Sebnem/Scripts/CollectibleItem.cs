using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public float interactRange = 3f;
    public AudioClip collectSound;
    public AudioSource audioSource;
     

    public float rotationSpeed = 100f;

    private void Update()
    {
        // Obje döndürülüyor
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right * rotationSpeed * 0.5f * Time.deltaTime, Space.Self);

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

        // Ses çal
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Yazýyý temizle
        if (interactText != null)
        {
            interactText.text = "";
        }

        //  Obje aktif ediliyor
        

        Destroy(gameObject, 0.1f);
    }
}
