using TMPro;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public TextMeshProUGUI yazi;
    public GameObject yat;
    public GameObject abla;
    public GameObject particle;

    private void Start()
    {
        particle.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yazi.text = "Press F";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("isCarry", false);
                BirakBuKariyi();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yazi.text = "";
        }
    }

    void BirakBuKariyi()
    {
        abla.transform.SetParent(null);
        abla.transform.SetParent(gameObject.transform);
        abla.transform.localPosition = Vector3.zero;
        abla.GetComponent<Animator>().SetBool("isCarry", false);
        particle.SetActive(true);
        Destroy(yazi.gameObject);
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

    }
}
