using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [Header("Görüþ ve Saldýrý")]
    public float visionRange = 15f;
    public float visionAngle = 120f;
    public float attackRange = 10f;
    public float attackCooldown = 3f;

    [Header("Büyü")]
    public GameObject spellPrefab;
    public Transform spellSpawnPoint;

    public Transform player;
    public int health = 100;
    public Slider healthSlider;

    [Header("Ölüm Ayarlarý")]
    public AudioClip deathSound; // Ölüm sesi
    private AudioSource audioSource; // Ses kaynaðý

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Saðlýk slider'ýný baþlat
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
    }

    void Update()
    {
        if (isDead) return; // Düþman öldüyse, hiçbir iþlem yapýlmasýn

        bool canSeePlayer = IsPlayerInSight();
        float distance = Vector3.Distance(transform.position, player.position);

        if (canSeePlayer && distance > attackRange)
        {
            FacePlayer();
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
        else if (canSeePlayer && distance <= attackRange)
        {
            // Saldýrý
            agent.isStopped = true;
            FacePlayer();

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                lastAttackTime = Time.time;
                Invoke(nameof(CastSpell), 0.5f); // Büyü animasyonla eþleþsin
            }
        }
        else
        {
            // Boþta
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
        }
    }

    
    public void GetDamage(int damage)
    {
        if (isDead) return; // Eðer düþman öldüyse, hasar almaz

        health -= damage; // Saðlýðý azalt

        // Saðlýk slider'ýný güncelle
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        // Saðlýk sýfýr veya daha düþükse ölme iþlemi baþlat
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    // Düþman öldüðünde yapýlacak iþlemler
    private void Die()
    {
        isDead = true; // Düþman öldü
        animator.SetBool("isDead", true); // Ölüm animasyonunu baþlat

        // Ölüm sesini çal
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // 3 saniye sonra düþmaný yok et
        Destroy(gameObject, 3f);
    }

    // Ölüm durumu
    public bool IsDead()
    {
        return isDead;
    }

    bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= visionRange && angle <= visionAngle / 2f)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, visionRange))
            {
                return hit.collider.CompareTag("Player");
            }
        }
        return false;
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y += 50;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void CastSpell()
    {
        if (spellPrefab != null && spellSpawnPoint != null)
        {
            Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        }
    }
}
