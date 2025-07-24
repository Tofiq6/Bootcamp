using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maksimum saðlýk
    private float currentHealth;    // Anlýk saðlýk
    public Slider healthSlider;     // UI'daki health slider
    public Animator playerAnimator; // Animasyon kontrolcüsü
    private bool isDead = false;    // Oyuncunun ölme durumu
    public TextMeshProUGUI demo;
    
    void Start()
    {
        // Baþlangýç saðlýk deðeri
        currentHealth = maxHealth;

        // Saðlýk slider'ýný güncelle
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

  
   
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return; 

        currentHealth -= damageAmount; 

        
        healthSlider.value = currentHealth;

        
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

  
    private void Die()
    {
        isDead = true; 
        playerAnimator.SetBool("isDead", true);

        DemoText();
    }

    
    private void DemoText()
    {

        demo.text = "Demoda ölemezsin!!!";
    }
}
