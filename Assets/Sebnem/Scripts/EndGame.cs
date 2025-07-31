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
        if (other.gameObject.CompareTag("Player"))
        {
            Etext.text = Estring;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        if (other.gameObject.CompareTag("Player"))
        {
            Etext.text = string.Empty;
        }
    }
}
