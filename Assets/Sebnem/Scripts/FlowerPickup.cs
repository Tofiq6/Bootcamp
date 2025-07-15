using UnityEngine;

public class FlowerPickup : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pickupUI;

    private bool isPlayerNear = false;

    private void Start()
    {
        if (pickupUI != null)
            pickupUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (pickupUI != null)
                pickupUI.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPlayerNear)
            {
                isPlayerNear = true;
                if (pickupUI != null)
                    pickupUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pickupUI != null)
                pickupUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.CollectFlower();
            Destroy(gameObject);
        }
    }
}
