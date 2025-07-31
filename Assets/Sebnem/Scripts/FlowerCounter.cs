using UnityEngine;
using TMPro;
using System.Collections;

public class FlowerCounter : MonoBehaviour
{
    public TextMeshProUGUI flowerCountText;
    private CanvasGroup canvasGroup;

    private int flowerCount = 0;
    public int totalFlowerCount = 10;

    private void Start()
    {
        DynamicTask.Instance.StartTask("PICKUP FLOWER", "Collect 10 moonlight blooms");
        canvasGroup = flowerCountText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = flowerCountText.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeInUI());
        UpdateFlowerCountText();
    }

    public void CollectFlower()
    {
        flowerCount++;
        UpdateFlowerCountText();

        if (flowerCount >= totalFlowerCount)
        {
            StartCoroutine(FadeOutUI());
            GameManager.Instance.isAllFlowersCollected = true;
        }
    }

    private void UpdateFlowerCountText()
    {
        flowerCountText.text = flowerCount + "/" + totalFlowerCount;
    }

    private IEnumerator FadeInUI()
    {
        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutUI()
    {
        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        flowerCountText.gameObject.SetActive(false);
    }
}
