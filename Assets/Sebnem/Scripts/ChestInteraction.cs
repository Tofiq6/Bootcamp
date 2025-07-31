using UnityEngine;
using TMPro;

public class ChestInteraction : MonoBehaviour
{
    public TextMeshProUGUI pressFText; // UI'da "Press F" mesajýný gösterecek TextMeshPro objesi

    void Start()
    {
        // Baþlangýçta "Press F" mesajýný gizle
        pressFText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.isChestOpened == true)
        {
            pressFText.gameObject.SetActive(true); // "Press F" mesajýný göster
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.isChestOpened == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject); // Chest objesini yok et
                DynamicTask.Instance.StartTask("ENCHANTED WAR", "Find the enchantment and enchant the sword");
                GameManager.Instance.swordInHand = true; // Kýlýcý oyuncuya ver
                GameManager.Instance.SaveGame(); // Oyun durumu kaydedilsin
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressFText.gameObject.SetActive(false); // "Press F" mesajýný gizle
        }
    }
}
