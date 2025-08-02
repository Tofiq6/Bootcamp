using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalTeleporter : MonoBehaviour
{
    public Transform targetPortal;
    public GameObject teleportEffectPrefab;
    public AudioSource audioSource;              // Eklenen: Ses kaynaðý
    public AudioClip teleportSound;              // Eklenen: Çalýnacak ses

    private bool isPlayerInside = false;

    private static Dictionary<GameObject, float> cooldownDict = new Dictionary<GameObject, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isPlayerInside) return;

        float currentTime = Time.time;

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

        // Ses efekti çal
        if (audioSource != null && teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        // Efekt baþlat
        if (teleportEffectPrefab != null)
        {
            GameObject effect = Instantiate(teleportEffectPrefab, player.transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        yield return new WaitForSeconds(1f);

        player.transform.position = targetPortal.position;

        cooldownDict[player.gameObject] = Time.time + 3f;

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
