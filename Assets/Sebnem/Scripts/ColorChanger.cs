using UnityEngine;

public class ColorChanger : MonoBehaviour, IInteractable
{
    private Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;  // MeshRenderer'ý alýp malzeme üzerinde iþlem yapacaðýz
    }

    public string GetDescription()
    {
        return "Press T to change to a random color";  // UI'de görünen açýklama
    }

    public void Interact()
    {
        // Nesnenin rengini rastgele deðiþtir
        mat.color = new Color(Random.value, Random.value, Random.value);
    }
}
