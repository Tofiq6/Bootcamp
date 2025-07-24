using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate((Vector3.forward + Vector3.left * 0.1f) * speed * Time.deltaTime);    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player büyü ile vuruldu!");

            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
            }

            
        }
    }

}
