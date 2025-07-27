using System.Collections.Generic;
using UnityEngine;

public class PasswordMission : MonoBehaviour
{
    public static PasswordMission Instance;

    [SerializeField] private List<int> correctOrder = new List<int> { 2, 0, 3, 1 };
    private List<int> currentOrder = new List<int>();

    [SerializeField] private List<PasswordObject> puzzleObjects; // Inspector üzerinden eklenir
    [SerializeField] private GameObject doorToOpen; // Açýlacak kapý

    private void Awake()
    {
        Instance = this;
    }

    public void ObjectActivated(int id)
    {
        currentOrder.Add(id);

        if (currentOrder.Count == correctOrder.Count)
        {
            if (IsCorrectOrder())
            {
                Debug.Log("Doðru sýra! Kapý açýlýyor.");
                OpenDoor();
            }
            else
            {
                Debug.Log("Yanlýþ sýra! Obje sýfýrlanýyor.");
                ResetPuzzle();
            }
        }
    }

    private bool IsCorrectOrder()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
                return false;
        }
        return true;
    }

    private void ResetPuzzle()
    {
        currentOrder.Clear();

        foreach (var obj in puzzleObjects)
        {
            obj.ResetObject();
        }
    }

    private void OpenDoor()
    {
        // Ýstersen animasyon veya baþka þeyler de tetikleyebilirsin
        if (doorToOpen != null)
            doorToOpen.SetActive(false); // Örn: kapýyý kaldýr
    }
}
