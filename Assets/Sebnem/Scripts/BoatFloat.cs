using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float floatDistance = 1f;        // Sandalýn gideceði mesafe (ileri-geri)
    public float floatSpeed = 1f;           // Hýz (daha yavaþ ya da daha hýzlý hareket için)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Sinüs dalgasýyla ileri-geri hareket
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatDistance;
        transform.position = startPos + transform.forward * offset;
    }
}
