using UnityEngine;
using TMPro;

public class carryCharacter : MonoBehaviour
{
    public GameObject character;
    public Animator animator;
    public Vector3 ayarVector;
    public TextMeshProUGUI yazi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraHugged)
        {
            yazi.text = "Press F";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraHugged)
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.GetComponent<Animator>().SetBool("isCarry", true);
                Kucakla();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.isLyraHugged)
        {
            yazi.text = "";
        }
    }

    public void Kucakla()
    {
        transform.position = character.transform.position + ayarVector;
        transform.Rotate(0, 90, 0);
        gameObject.transform.SetParent(character.transform);
        animator.SetBool("isCarry", true);
        GameManager.Instance.isLyraHugged = true;
        GameManager.Instance.SaveGame();
    }
}