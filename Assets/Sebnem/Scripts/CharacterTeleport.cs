using UnityEngine;
using System.Collections;

public class PortalTeleporter : MonoBehaviour
{
    public Transform targetPortal;
    public GameObject teleportEffectPrefab;

    private bool isTeleporting = false;
    private float cooldownTime = 3f; // Iþýnlandýktan sonra tekrar ýþýnlanmamasý için süre

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag("Player") && targetPortal != null)
        {
            StartCoroutine(TryTeleport(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Oyuncu portaldan çýkýnca tekrar ýþýnlanabilir hale gelsin
        if (other.CompareTag("Player"))
        {
            isTeleporting = false;
        }
    }

    IEnumerator TryTeleport(Collider player)
    {
        isTeleporting = true;

        // Particle efekti oynat
        if (teleportEffectPrefab != null)
        {
            Instantiate(teleportEffectPrefab, player.transform.position, Quaternion.identity);
        }

        // 1 saniye bekle
        yield return new WaitForSeconds(1f);

        // Iþýnla
        player.transform.position = targetPortal.position;

        // Diðer portalda 3 saniye ýþýnlanma engeli olsun
        PortalTeleporter otherPortal = targetPortal.GetComponent<PortalTeleporter>();
        if (otherPortal != null)
        {
            otherPortal.StartCoroutine(otherPortal.Cooldown());
        }

        // Bu portal için de 3 saniye sonra tekrar ýþýnlanabilir hale gelecek
        yield return new WaitForSeconds(cooldownTime);
        isTeleporting = false;
    }

    IEnumerator Cooldown()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(cooldownTime);
        isTeleporting = false;
    }
}
