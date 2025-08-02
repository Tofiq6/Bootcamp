using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    public VideoClip clip;
    public VideoPlayer player;
    public TextMeshProUGUI Etext;
    public string Estring;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.isGameEnd)
        {
            Etext.text = Estring;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.isGameEnd)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.clip = clip;
                player.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.isGameEnd)
        {
            Etext.text = string.Empty;
        }
    }
}
