using UnityEngine;
using TMPro;
using System.Collections;
public class MapItem : MonoBehaviour
{
    public AudioClip collectSound;  // Toplama sesi
    public GameObject imageToActivate; // Görseli aktif etmek için
    public TextMeshProUGUI mapCountText; // Map count textini güncellemek için
    public int totalMaps = 3; // Toplanmasý gereken toplam obje sayýsý
    private int collectedMaps = 0; // Þu anki toplanan obje sayýsý

    private void Start()
    {
        // Görseli baþta devre dýþý býrak
        if (imageToActivate != null)
        {
            imageToActivate.SetActive(false);
        }
    }

    // Player objesi trigger'a girdiðinde çalýþacak
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer Player objesiyle temas ettiyse
        {
            // Ses çal
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Animasyonu baþlat (Yükselip alçalacak)
            StartCoroutine(PlayFloatingAnimation());

            // Objeyi yok et
            Destroy(gameObject);

            // Map count'u güncelle
            UpdateMapCount();
        }
    }

    // Map count'u güncelleyen fonksiyon
    private void UpdateMapCount()
    {
        collectedMaps++; // Toplanan harita sayýsýný arttýr
        if (mapCountText != null && collectedMaps != 3)
        {
            mapCountText.text = collectedMaps + "/" + totalMaps; // 0/3 gibi
        }
        else if (collectedMaps >= 3)
        {
            imageToActivate.SetActive(true);
        }
    }

    // Objeye inip kalkma animasyonu eklemek için
    private IEnumerator PlayFloatingAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0.5f, 0); // Hafif yükselme
        float elapsedTime = 0f;
        float duration = 0.5f; // Yükselip alçalma süresi

        // Yükselme
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        // Ýniþ
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
