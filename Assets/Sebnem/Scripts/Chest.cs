using UnityEngine;
using TMPro;  // TextMeshPro için gerekli
using System.Collections;
public class Chest : MonoBehaviour
{
    public TextMeshProUGUI interactText;   // Ekranda "Press E to Open" veya "Chest Locked" yazacak olan TextMeshPro
    public GameObject chestLid;            // Chest'in kapaðý (açýlacak olan kýsmý)
    public float interactRange = 3f;       // Objeye yaklaþmak için gereken mesafe
    private bool playerInRange = false;   // Oyuncu chest trigger'ýna girdi mi?

    private void Update()
    {
        // Eðer oyuncu chest'in trigger'ýna girdiyse
        if (playerInRange)
        {
            // Ekranda "Press E" veya "Chest Locked" mesajýný göster
            if (interactText != null)
            {
                if (GameManager.Instance.haveKey)  // GameManager'dan key kontrolü
                {
                    interactText.text = "Press E to Open Chest";
                }
                else
                {
                    interactText.text = "Chest Locked";
                }
            }

            // Eðer E tuþuna basýlýrsa chest'i aç
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckAndOpenChest();
            }
        }
        else
        {
            // Mesafe uzaksa mesajý gizle
            if (interactText != null)
            {
                interactText.text = "";
            }
        }
    }

    // Chest'i açma fonksiyonu
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

    // Chest'i açma fonksiyonu
    private void OpenChest()
    {
        // Kapaðýn açýlmasý
        StartCoroutine(OpenChestLidCoroutine());

        // Kapanacak mesajý ver
        Debug.Log("Chest Opened!");
    }

    // Chest kapaðýný açan coroutine
    private IEnumerator OpenChestLidCoroutine()
    {
        float openSpeed = 2f; // Kapaðýn açýlma hýzý
        Vector3 targetRotation = new Vector3(0, 90, 0); // Kapaðýn açýlacaðý hedef açý (Y ekseninde 90 derece)
        Quaternion startRotation = chestLid.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            chestLid.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime * openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chestLid.transform.rotation = endRotation;  // Son pozisyonu ayarla
    }

    // Oyuncu chest'in trigger'ýna girdiðinde çalýþacak
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer player tag'li obje ile çarpýþýrsa
        {
            playerInRange = true;  // Oyuncu chest'e yakýn
        }
    }

    // Oyuncu chest'in trigger'ýndan çýktýðýnda çalýþacak
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer player tag'li obje trigger'dan çýkarsa
        {
            playerInRange = false;  // Oyuncu chest'ten uzaklaþtý
        }
    }
}
