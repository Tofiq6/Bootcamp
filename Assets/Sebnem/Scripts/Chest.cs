using UnityEngine;
using TMPro;  // TextMeshPro için gerekli
using System.Collections;
public class Chest : MonoBehaviour
{
    public TextMeshProUGUI interactText;   // Ekranda "Press E to Open" veya "Chest Locked" yazacak olan TextMeshPro
    public GameObject chestClosed;   
    public GameObject chestOpen;

    private void Start()
    {
        chestOpen.SetActive(false);
        chestClosed.SetActive(true);
    }

    private void CheckAndOpenChest()
    {
        // GameManager'dan anahtar durumu kontrolü
        if (GameManager.Instance.haveKey)
        {
            OpenChest();
        }
        else
        {
            // Eðer anahtar yoksa, locked mesajý göster
            Debug.Log("Chest is locked. You need a key!");
        }
    }

    // Chest kapaðýný açan coroutine
    private void OpenChest()
    {
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
        GameManager.Instance.isChestOpened = true; // Chest açýldý olarak iþaretle
    }

    // Oyuncu chest'in trigger'ýna girdiðinde çalýþacak
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.haveKey)
            {
                interactText.text = "Press E to Open Chest";
            }
            else
            {
                interactText.text = "Chest Locked";
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckAndOpenChest();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.haveKey) 
            {
                interactText.text = "";
            }
            else
            {
                interactText.text = "";
            }
        }
    }
}
