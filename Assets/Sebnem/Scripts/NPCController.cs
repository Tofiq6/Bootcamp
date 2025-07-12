using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] waypoints;
    private int currentTarget = 0;

    public float waitTime = 2f;
    private bool isWaiting = false;

    private Animator animator;

    public Transform player;
    public float detectionRange = 5f;
    private bool playerInRange = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetWalking(true);
    }

    void Update()
    {
        // Oyuncu kontrolü
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                ReactToPlayer(); // Oyuncuyu fark edince ne yapacaðýný tanýmlar
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                ResumePatrol(); // Oyuncu uzaklaþýnca tekrar devriyeye baþla
            }
        }

        // Patrol yapma
        if (!isWaiting && !playerInRange)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 direction = waypoints[currentTarget].position - transform.position;
        direction.y = 0f;

        if (direction.magnitude < 0.1f)
        {
            StartCoroutine(WaitAndNextPoint());
            return;
        }

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        transform.forward = direction.normalized;
    }

    IEnumerator WaitAndNextPoint()
    {
        SetWalking(false);
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentTarget = (currentTarget + 1) % waypoints.Length;
        SetWalking(true);
        isWaiting = false;
    }

    void SetWalking(bool walking)
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", walking);
        }
    }

    void ReactToPlayer()
    {
        StopAllCoroutines();
        SetWalking(false);
        isWaiting = true;

        // Oyuncuya bak (dön)
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        transform.forward = lookDirection.normalized;

        // Tepki animasyonu varsa tetikle (örneðin el sallama, þaþýrma vb.)
        // animator.SetTrigger("Wave"); // örnek
    }

    void ResumePatrol()
    {
        SetWalking(true);
        isWaiting = false;
    }
}
