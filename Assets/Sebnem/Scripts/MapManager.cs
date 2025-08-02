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
    public GameObject objectToActivate; //  3/3 olunca aktif edilecek ekstra obje

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (buttonToActivate != null)
            buttonToActivate.gameObject.SetActive(false); // Baþlangýçta kapalý

        if (objectToActivate != null)
            objectToActivate.SetActive(false); //  Baþlangýçta kapalý
    }

    public void CollectMap()
    {
        collectedMaps++;

        if (mapCountText != null && collectedMaps < totalMaps)
        {
            mapCountText.text = collectedMaps + "/" + totalMaps;
        }

        if (collectedMaps >= totalMaps)
        {
            if (mapCountText != null)
                mapCountText.text = totalMaps + "/" + totalMaps;

            if (buttonToActivate != null)
                buttonToActivate.gameObject.SetActive(true); // Butonu aktif et

            if (objectToActivate != null)
                objectToActivate.SetActive(true); //  Ekstra objeyi aktif et
        }
    }
}
