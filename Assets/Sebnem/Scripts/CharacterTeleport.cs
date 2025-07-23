using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalTeleporter : MonoBehaviour
{
    public Transform targetPortal;
    public GameObject teleportEffectPrefab;

    private bool isPlayerInside = false;

    // Her oyuncu için cooldown tutulur
    private static Dictionary<GameObject, float> cooldownDict = new Dictionary<GameObject, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isPlayerInside) return;

        float currentTime = Time.time;

        // Eðer bu oyuncu için cooldown varsa ve süresi geçmemiþse
        if (cooldownDict.ContainsKey(other.gameObject) && currentTime < cooldownDict[other.gameObject])
        {
            return;
        }

        if (targetPortal != null)
        {
            StartCoroutine(TeleportWithDelay(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    IEnumerator TeleportWithDelay(Collider player)
    {
        isPlayerInside = true;

        // Efekt baþlat
        if (teleportEffectPrefab != null)
        {
            GameObject effect = Instantiate(teleportEffectPrefab, player.transform.position, Quaternion.identity);
            Destroy(effect, 1f); // Efekti 1 saniye sonra yok et
        }

        yield return new WaitForSeconds(1f); // Efekt süresi kadar bekle

        // Iþýnla
        player.transform.position = targetPortal.position;

        // Cooldown baþlat (3 saniye)
        cooldownDict[player.gameObject] = Time.time + 3f;

        // Diðer portal da oyuncunun içerde olduðunu bilsin
        PortalTeleporter otherPortal = targetPortal.GetComponent<PortalTeleporter>();
        if (otherPortal != null)
        {
            otherPortal.MarkPlayerInside();
        }
    }

    public void MarkPlayerInside()
    {
        isPlayerInside = true;
    }
}
