using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PasswordObject : MonoBehaviour, IInteractable
{
    public int objectID; // Her bir objeye benzersiz ID ver (0,1,2,3 gibi)
    bool isActivated = false;
    public GameObject Torch;

    void Start()
    {
        Torch.SetActive(false);
    }

    public void Activate()
    {
        if (isActivated) return;

        isActivated = true;
        Torch.SetActive(true);
        PasswordMission.Instance.ObjectActivated(objectID);
        // Görsel olarak aktif hale getir (örneðin ýþýk yanmasý vs.)
        Debug.Log("Obje aktif: " + objectID);
    }

    public string GetDescription()
    {
        return "Press T to interact";
    }

    public void Interact()
    {
        Activate();
    }

    public void ResetObject()
    {
        isActivated = false;
        // Görsel olarak pasif hale getir (ýþýk sönmesi vs.)
        Debug.Log("Obje sýfýrlandý: " + objectID);
    }
}
