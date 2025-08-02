using UnityEngine;
using TMPro;
using System.Collections;

public class PanelToggleWithKeyAndMessage : MonoBehaviour
{
    public GameObject panelToToggle;         // Açýlýp kapanacak panel
    public GameObject controllingButton;     // Butonun aktifliðini kontrol eder
    public TextMeshProUGUI infoText;         // "Press M to open the Map" yazýsý

    private bool isPanelOpen = false;
    private bool hasShownText = false;

    void Update()
    {
        if (controllingButton != null)
        {
            // Buton aktif ve yazý daha önce gösterilmediyse
            if (controllingButton.activeInHierarchy && infoText != null && !hasShownText)
            {
                infoText.text = "Press M to open the Map";
                infoText.gameObject.SetActive(true);
                hasShownText = true;
                StartCoroutine(HideInfoTextAfterDelay(4f)); // 4 saniye sonra yazýyý gizle
            }

            // M tuþuna basýnca paneli aç/kapat
            if (controllingButton.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
            {
                isPanelOpen = !isPanelOpen;
                panelToToggle.SetActive(isPanelOpen);
            }
        }

        // Buton devre dýþýysa yazýyý gizle
        if (controllingButton != null && !controllingButton.activeInHierarchy && infoText != null && infoText.gameObject.activeSelf)
        {
            infoText.gameObject.SetActive(false);
        }
    }

    private IEnumerator HideInfoTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
        }
    }
}
