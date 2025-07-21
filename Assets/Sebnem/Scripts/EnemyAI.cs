using UnityEngine;
using UnityEngine.AI;

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

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player bulunamadý! Sahnedeki player objesine 'Player' tag'i ver.");
            enabled = false;
            return;
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

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
