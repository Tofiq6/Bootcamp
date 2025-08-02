using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public int totalMaps = 3;
    public int collectedMaps = 0;

    public TextMeshProUGUI mapCountText;
    public Button buttonToActivate; // 3/3 olduðunda aktif olacak buton

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (buttonToActivate != null)
            buttonToActivate.gameObject.SetActive(false); // Baþlangýçta kapalý
    }

    public void CollectMap()
    {
        collectedMaps++;

        if (mapCountText != null && collectedMaps < totalMaps)
        {
            mapCountText.text = collectedMaps + "/" + totalMaps;
        }

        if (collectedMaps >= totalMaps && buttonToActivate != null)
        {
            mapCountText.text = totalMaps + "/" + totalMaps;
            buttonToActivate.gameObject.SetActive(true); // Butonu aktif et
        }
    }
}
