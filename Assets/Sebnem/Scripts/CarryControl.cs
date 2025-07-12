using UnityEngine;

public class CarryControl : MonoBehaviour
{
    public GameObject indirilecekyer;
    public GameObject character;
    public Animator animator;

    public Vector3 ayarVector;
    public bool kucakla = false;
    public bool birak = false;
   
   

    
    void Update()
    {

        if (kucakla)
        {
            transform.position= character.transform.position+ayarVector;
            animator.SetBool("isCarry", true);
            
            kucakla = false;
           
        }
       

        if (birak) {

            transform.parent = null;
            animator.SetBool("isCarry",false);
            transform.position= indirilecekyer.transform.position;
            birak = false;

        }

       

    }
}





