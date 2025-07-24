using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damageAmount = 10; // Kýlýcýn vereceði hasar
    public AudioClip hitSound; // Çarpýþma sesi
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ses kaynaðýný al
    }

    // Bu fonksiyon kýlýcýn çarpýþtýðý objede tetiklenecek
    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþan obje "Enemy" tag'ine sahipse
        if (other.CompareTag("Enemy"))
        {
            // EnemyAI script'ine eriþip hasar ver
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();

            if (enemyAI != null)
            {
                enemyAI.GetDamage(damageAmount); // Düþmana hasar ver

                // Çarpýþma sesini çal
                if (audioSource != null && hitSound != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }
            }
        }
    }
}
