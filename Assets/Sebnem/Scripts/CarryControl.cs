using UnityEngine;

public class CarryControl : MonoBehaviour
{
   
    public GameObject character;
    public Animator animator;
    public Vector3 ayarVector;

    public void Kucakla()
    {
        transform.position = character.transform.position + ayarVector;
        transform.Rotate(0, 90, 0);
        gameObject.transform.SetParent(character.transform);
        animator.SetBool("isCarry", true);                    
    }
}





