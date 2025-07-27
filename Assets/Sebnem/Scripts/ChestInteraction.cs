using UnityEngine;
using TMPro;

public class ChestInteraction : MonoBehaviour
{
    public TextMeshProUGUI pressFText; // UI'da "Press F" mesajýný gösterecek TextMeshPro objesi
    private bool isPlayerNear = false;

    void Start()
    {
        // Baþlangýçta "Press F" mesajýný gizle
        pressFText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Eðer oyuncu chest'e yakýnsa ve chest açýlmamýþsa "F" tuþuna basýldýðýnda objeyi yok et ve kýlýcý oyuncuya ver
        if (isPlayerNear && GameManager.Instance.isChestOpened == false)
        {
            pressFText.gameObject.SetActive(true); // "Press F" mesajýný göster

            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject); // Chest objesini yok et
                GameManager.Instance.swordInHand = true; // Kýlýcý oyuncuya ver
                GameManager.Instance.isChestOpened = true; // Chest açýldý olarak iþaretle
                GameManager.Instance.SaveGame(); // Oyun durumu kaydedilsin
            }
        }
        else
        {
            pressFText.gameObject.SetActive(false); // "Press F" mesajýný gizle
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; // Oyuncu chest'e yaklaþtý
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false; // Oyuncu chest'ten uzaklaþtý
        }
    }
}
