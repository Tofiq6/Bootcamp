using UnityEngine;
using System.Collections;

public class TriggerLift : MonoBehaviour
{
    public GameObject[] objectsToLift;  // Yükselecek objeler
    public float targetY = 5f;          // Hedef Y pozisyonu
    public float liftSpeed = 1f;        // Yavaþça yükselme hýzý
    public AudioClip liftSound;         // Yükselme sýrasýnda çalacak ses
    public bool isLifted = false;
    public bool delay = false;
    public float delayTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Eðer player trigger'a girdiðinde
        if (other.CompareTag("Player") && !isLifted) // Player tag'ine sahip olmalý
        {
            // Ses kaydýný çal
            if (liftSound != null)
            {
                AudioSource.PlayClipAtPoint(liftSound, transform.position);
            }

            // Coroutine ile objeleri kaldýr
            foreach (GameObject obj in objectsToLift)
            {
                StartCoroutine(LiftObject(obj));                
            }
        }
    }

    
    public IEnumerator LiftObject(GameObject obj)
    {
        if (delay)
        {
            yield return new WaitForSeconds(delayTime);
        }

        Vector3 startPosition = obj.transform.position;
        float targetYPosition = targetY;

        
        while (obj.transform.position.y < targetYPosition)
        {
            float newY = Mathf.Lerp(obj.transform.position.y, targetYPosition, Time.deltaTime * liftSpeed);
            obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);
            yield return null;
        }

        
        obj.transform.position = new Vector3(obj.transform.position.x, targetYPosition, obj.transform.position.z);
        isLifted = true;
    }
}
