using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro için namespace

public class DynamicTask : MonoBehaviour
{
    public static DynamicTask Instance;

    public DynamicChaseTrigger dynamicChase;
    public GameObject imageObject; // Görseli aktif etmek için
    public TextMeshProUGUI text1; // TextMeshPro 1
    public TextMeshProUGUI text2; // TextMeshPro 2
    public Vector2 targetPosition; // Hedef konum (2D)
    public float slideDuration = 1f; // Kayma süresi
    public float displayTime = 3f; // Ekranda kalma süresi

    private RectTransform rectTransform; // RectTransform
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde yok olma
        }
        else
        {
            Destroy(gameObject); // Zaten varsa yenisini yok et
        }
    }
    private void Start()
    {
        rectTransform = imageObject.GetComponent<RectTransform>();
    }

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
        Vector2 startPosition = new Vector2(-450, -200); // Sol taraftan baþla
        Vector2 endPosition = targetPosition;

        rectTransform.anchoredPosition = startPosition;

        // Kayma efektini baþlat
        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, (elapsedTime / slideDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ekranda belirli bir süre kal
        yield return new WaitForSeconds(displayTime);

        // Kayma efekti ile sola kayarak yok olma
        Vector2 startExitPosition = rectTransform.anchoredPosition;
        Vector2 exitPosition = new Vector2(-450, -200); // Ekrandan kaybolacak konum

        elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startExitPosition, exitPosition, (elapsedTime / slideDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageObject.SetActive(false);
    }
}
