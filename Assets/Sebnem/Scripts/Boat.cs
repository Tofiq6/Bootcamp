using TMPro;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public TextMeshProUGUI yazi;
    public GameObject Lyra;
    public GameObject particle;
    public GameObject objectToActivate1;
    public GameObject objectToActivate2;

    public AudioSource audioSource;      // Eklenen: Audio kaynaðý
    public AudioClip activateSound;      // Eklenen: Çalýnacak ses efekti

    private void Start()
    {
        particle.SetActive(false);
        objectToActivate1.SetActive(false);
        objectToActivate2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat && GameManager.Instance.isLyraHugged)
        {
            yazi.text = "Press F";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat && GameManager.Instance.isLyraHugged)
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.GetComponent<Animator>().SetBool("isCarry", false);
                Destroy(Lyra);
                GameManager.Instance.isLyraHugged = false;
                GameManager.Instance.isLyraInBoat = true;
                GameManager.Instance.SaveGame();
                yazi.text = "";
                LyraInTheBoat();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat && GameManager.Instance.isLyraHugged)
        {
            yazi.text = "";
        }
    }

    public void LyraInTheBoat()
    {
        particle.SetActive(true);

        if (GameManager.Instance.isLyraInBoat)
        {
            if (objectToActivate1 != null) objectToActivate1.SetActive(true);
            if (objectToActivate2 != null) objectToActivate2.SetActive(true);

            // Ses efekti çal
            if (audioSource != null && activateSound != null)
            {
                audioSource.PlayOneShot(activateSound);
            }
        }
    }
}
