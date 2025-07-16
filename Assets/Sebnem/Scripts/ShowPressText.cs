using UnityEngine;
using TMPro;

public class ShowPressFText : MonoBehaviour
{
    public TMP_Text pressFText;   // Inspector'dan sürükle (UI'deki TextMeshPro)
    public string message = "Press 'F'";

    private void Start()
    {
        if (pressFText != null)
            pressFText.gameObject.SetActive(false);  // Baþlangýçta gizle
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pressFText != null)
        {
            pressFText.text = message;
            pressFText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && pressFText != null)
        {
            pressFText.gameObject.SetActive(false);
        }
    }
}
