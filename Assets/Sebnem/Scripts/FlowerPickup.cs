using UnityEngine;
using TMPro; // TextMesh Pro kullanýmý için gerekli.

public class FlowerPickup : MonoBehaviour
{
    public GameManager gameManager; // GameManager'ý referans alýyoruz.
    public GameObject pickupUI; // UI gösterilecek nesne.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer oyuncu çiçeðe yakýnsa
        {
            pickupUI.SetActive(true); // Ekranda "E" tuþu ile toplama yönergesini göster
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer oyuncu çiçekten uzaklaþýrsa
        {
            pickupUI.SetActive(false); // UI'yi gizle
        }
    }

    private void Update()
    {
        if (pickupUI.activeSelf && Input.GetKeyDown(KeyCode.E)) // Eðer UI aktifse ve E'ye basýlmýþsa
        {
            gameManager.CollectFlower(); // Çiçek toplama fonksiyonunu çaðýr.
            Destroy(gameObject); // Çiçek objesini yok et.
        }
    }
}
