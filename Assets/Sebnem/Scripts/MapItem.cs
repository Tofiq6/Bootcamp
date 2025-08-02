using UnityEngine;
using System.Collections;

public class MapItem : MonoBehaviour
{
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            StartCoroutine(PlayFloatingAnimation());
            MapManager.Instance.CollectMap();
            Destroy(gameObject, 0.5f);
        }
    }

    private IEnumerator PlayFloatingAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0.5f, 0);
        float duration = 0.25f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
