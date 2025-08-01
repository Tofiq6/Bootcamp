using UnityEngine;
using UnityEngine.UI;

public class UIPanelController : MonoBehaviour
{
    [System.Serializable]
    public class PanelGroup
    {
        public GameObject panel;      // Panel objesi
        public Button openButton;     // Açma butonu
        public Button closeButton;    // Kapatma butonu
    }

    public PanelGroup[] panels; // Panel gruplarýný Inspector'dan ayarlayacaðýz

    void Start()
    {
        foreach (var group in panels)
        {
            if (group.openButton != null && group.panel != null)
                group.openButton.onClick.AddListener(() => ShowPanel(group));

            if (group.closeButton != null && group.panel != null)
                group.closeButton.onClick.AddListener(() => HidePanel(group));

            // Baþlangýçta paneller kapalý olabilir
            group.panel.SetActive(false);
        }
    }

    private void ShowPanel(PanelGroup group)
    {
        group.panel.SetActive(true);
    }

    private void HidePanel(PanelGroup group)
    {
        group.panel.SetActive(false);
    }
}
