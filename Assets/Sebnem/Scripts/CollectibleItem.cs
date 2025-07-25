using UnityEngine;
using TMPro;  

public class CollectibleItem : MonoBehaviour
{
    public TextMeshProUGUI interactText;             
    public float interactRange = 3f;       
    public AudioClip collectSound;         

    private void Update()
    {
        
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= interactRange)
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
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position); 
        }

        Destroy(gameObject); 
    }
}
