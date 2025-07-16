using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class NPCController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] waypoints;
    private int currentTarget = 0;

    public float waitTime = 2f;
    private bool isWaiting = false;

    public Animator animator;
    private Rigidbody rb;

    public Transform player;
    public float detectionRange = 5f;
    private bool playerInRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Dönme engellensin

        SetWalking(true);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                ReactToPlayer();
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                ResumePatrol();
            }
        }

        if (!isWaiting && !playerInRange)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentTarget].position;
        Vector3 moveDirection = targetPosition - transform.position;
        moveDirection.y = 0f; // Y ekseninde hareket etme!

        if (moveDirection.magnitude < 0.1f)
        {
            StartCoroutine(WaitAndNextPoint());
            return;
        }

        // Rigidbody ile hareket
        Vector3 movement = moveDirection.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        // Yüzünü hedefe çevir
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
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

        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            transform.forward = lookDir.normalized;
    }

    void ResumePatrol()
    {
        SetWalking(true);
        isWaiting = false;
    }
}
