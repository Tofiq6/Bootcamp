using UnityEngine;
using TMPro; // TextMesh Pro'yu kullanmak için gerekli

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI flowerCountText; // TextMesh Pro'yu kullanýyoruz.
    private int flowerCount = 0; // Toplanan çiçek sayýsý.

    private void Start()
    {
        UpdateFlowerCountText(); // Baþlangýçta sayýyý güncelle.
    }

    public void CollectFlower()
    {
        flowerCount++; // Çiçek sayýsýný arttýr.
        UpdateFlowerCountText(); // UI'yi güncelle.
    }

    private void UpdateFlowerCountText()
    {
        flowerCountText.text = flowerCount + "/10"; // Çiçek sayýsýný UI'da göster.
    }
}
