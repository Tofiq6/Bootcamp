using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class carryCharacter : MonoBehaviour
{
    public TextMeshProUGUI yazi;
    public CarryControl controlCarry;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yazi.text = "Press F";
            if (Input.GetKeyDown(KeyCode.F) && yazi != null)
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("isCarry", true);
                controlCarry.Kucakla();
                Destroy(yazi.gameObject);  
            }
        }
    }
}
