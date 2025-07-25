using UnityEngine;
using TMPro;  // TextMeshPro için gerekli

public class ButtonPanelManager : MonoBehaviour
{
    public TextMeshProUGUI mapCountText; // Map count textini alacaðýmýz TextMeshPro
    public GameObject button;           // Buton objesi
    public GameObject panel;            // Açýlacak panel
    public GameObject exitButton;       // Paneldeki Exit butonu

    private void Start()
    {
        // Baþlangýçta butonu devre dýþý býrak
        button.SetActive(false);

        // Eðer paneldeki exit butonuna týklanýrsa, paneli kapat
        if (exitButton != null)
        {
            exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ClosePanel);
        }
    }

    private void Update()
    {
        // Eðer mapCountText "3/3" ise butonu aktif et
        if (mapCountText.text == "3/3")
        {
            button.SetActive(true);
        }
    }

    // Butona týklanýnca paneli aç
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);  // Paneli aç
        }
    }

    // Exit butonuna týklanýnca paneli kapat
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Paneli kapat
        }
    }
}
