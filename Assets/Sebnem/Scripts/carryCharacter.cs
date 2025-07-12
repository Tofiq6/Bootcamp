using UnityEngine;
using TMPro;
public class carryCharacter : MonoBehaviour
{

    public TextMeshProUGUI yazi;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yazi.text = "Press F";
            if (Input.GetKeyDown(KeyCode.F))
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("isCarry", true);
            }
        }
    }
}
