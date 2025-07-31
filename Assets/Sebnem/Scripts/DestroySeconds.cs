using UnityEngine;

public class DestroySeconds : MonoBehaviour
{
    public float seconds;

    void Start()
    {
        Invoke("DestroyObject", seconds);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
