using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player; // Oyuncu karakteri
    public float followDistance = 3.0f; // Takip mesafesi
    public float followSpeed = 3.0f; // Takip hýzý
    public Animator followerAnimator; // Takipçi animatörü
    private bool isFollowing = false; // Takip baþlama durumu

    private void Update()
    {
        Debug.Log(isFollowing);
        if (isFollowing)
        {
            FollowPlayer(); // Oyuncuyu takip et
        }
    }

    private void FollowPlayer()
    {
        // Takipçi karakterinin oyuncudan belirli bir mesafede kalmasýný saðla
        Vector3 targetPosition = player.transform.position - player.transform.forward * followDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    public void StartFollowing()
    {
        isFollowing = true; // Takip etmeye baþla
    }

    public void StopFollowing()
    {
        isFollowing = false; // Takibi durdur
    }
}
