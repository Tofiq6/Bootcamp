using TMPro;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public TextMeshProUGUI yazi;
    public GameObject yat;
    public GameObject Lyra;
    public GameObject particle;
    public GameObject objectToActivate;  

    private void Start()
    {
        particle.SetActive(false);
        yat.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat)
        {
            yazi.text = "Press F";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat)
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.GetComponent<Animator>().SetBool("isCarry", false);
                Destroy(Lyra); // Lyra'yý yok et
                GameManager.Instance.isLyraHugged = false; 
                GameManager.Instance.isLyraInBoat = true;  
                GameManager.Instance.SaveGame();           
                LyraInTheBoat();                          
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraInBoat)
        {
            yazi.text = "";
        }
    }

    
    public void LyraInTheBoat()
    {
        particle.SetActive(true);
        yat.SetActive(true);
        yat.GetComponent<Animator>().SetBool("isCarry", false);

     
        AudioSource audioSource = yat.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        
        if (GameManager.Instance.isLyraInBoat && objectToActivate != null)
        {
            objectToActivate.SetActive(true); 
        }
    }
}
