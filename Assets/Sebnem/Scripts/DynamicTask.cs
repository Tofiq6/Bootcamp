using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro için namespace

public class DynamicTask : MonoBehaviour
{
    public GameObject imageObject; // Görseli aktif etmek için
    public TextMeshProUGUI text1; // TextMeshPro 1
    public TextMeshProUGUI text2; // TextMeshPro 2
    public Vector3 targetPosition; // Hedef konum
    public float slideDuration = 1f; // Kayma süresi
    public float displayTime = 3f; // Ekranda kalma süresi

    // Bu metodu çaðýrarak görevi baþlatabilirsiniz
    public void StartTask(string newText1, string newText2)
    {
        StartCoroutine(TaskRoutine(newText1, newText2));
    }

    private IEnumerator TaskRoutine(string newText1, string newText2)
    {
        // Resmi aktif et
        imageObject.SetActive(true);

        // TextMeshPro metinlerini güncelle
        text1.text = newText1;
        text2.text = newText2;

        // Kayma efekti ile ekrana gelme
        Vector3 startPosition = new Vector3(-Screen.width, targetPosition.y, targetPosition.z); // Sol taraftan baþla
        Vector3 endPosition = targetPosition;

        imageObject.transform.position = startPosition;

        // Kayma efektini baþlat
        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            imageObject.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / slideDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ekranda belirli bir süre kal
        yield return new WaitForSeconds(displayTime);

        // Kayma efekti ile sola kayarak yok olma
        Vector3 startExitPosition = imageObject.transform.position;
        Vector3 exitPosition = new Vector3(-Screen.width, targetPosition.y, targetPosition.z); // Ekrandan kaybolacak konum

        elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            imageObject.transform.position = Vector3.Lerp(startExitPosition, exitPosition, (elapsedTime / slideDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Resmi deaktif et
        imageObject.SetActive(false);
    }
}
