using UnityEngine;

public class AudioClose : MonoBehaviour
{
    [SerializeField] private GameObject objectToDeactivate;

    public void CloseAudioObject()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("AudioClose: objectToDeactivate is not assigned.");
        }
    }
}
