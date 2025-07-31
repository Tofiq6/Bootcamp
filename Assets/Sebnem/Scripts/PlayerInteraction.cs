using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class PlayerInteraction : MonoBehaviour
{

    public Camera mainCam;  // Oyuncunun kamerasý
    public float interactionDistance = 20f;  // Etkileþim mesafesi
    public LayerMask layersToHit;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    private void Update()
    {
        InteractionRay();
    }

    // Raycast ile etkileþim kontrolü
    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);  // Kameranýn ortasýna doðru bir ýþýn gönder
        RaycastHit hit;

        bool hitSomething = false;

        // Eðer bir nesneye çarptýysak ve mesafe uygun ise
        if (Physics.Raycast(ray, out hit, interactionDistance, layersToHit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();  // Çarpýlan nesnede IInteractable var mý?

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();  // Etkileþime girilebilecek nesnenin açýklamasýný göster

                if (Input.GetKeyDown(KeyCode.T))
                {
                    interactable.Interact();  // Etkileþim metodunu çaðýr
                }
            }
        }

        interactionUI.SetActive(hitSomething);  // Etkileþim UI'ýný aktif et
    }
}
