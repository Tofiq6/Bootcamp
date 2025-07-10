using UnityEngine;
using System.Collections; // IEnumerator için gerekli

public class Follower : MonoBehaviour
{
    public GameObject player; // Oyuncu karakteri
    public float followDistance = 3.0f; // Takip mesafesi
    public float followSpeed = 3.0f; // Takip hýzý
    public Animator followerAnimator; // Takipçi animatörü
    private bool isFollowing = false; // Takip baþlama durumu

    public GameObject particleEffectPrefab; // Partikül efekti prefab'ý
    private GameObject particleEffectInstance; // Efektin örneði

    private void Start()
    {
        // Baþlangýçta takipçi inaktif olacak
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isFollowing)
        {
            FollowPlayer(); // Oyuncuyu takip et
            LookAtPlayer(); // Oyuncuya doðru dön
        }
    }

    private void FollowPlayer()
    {
        // Takipçi karakterinin oyuncudan belirli bir mesafede kalmasýný saðla
        Vector3 targetPosition = player.transform.position - player.transform.forward * followDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        // Oyuncuya doðru dönmesi için karakteri yönlendir
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition); // Yalnýzca X ve Z eksenlerinde bakacak þekilde yönlendirilir
    }

    public void StartFollowing()
    {
        isFollowing = true; // Takip etmeye baþla
        gameObject.SetActive(true); // Takipçi aktif olmalý
    }

    public void StopFollowing()
    {
        StartCoroutine(Disappear()); // Takipçi yok olma sürecini baþlat
    }

    private IEnumerator Disappear()
    {
        // Yok olmadan önce partikül efektini baþlat
        if (particleEffectPrefab != null)
        {
            // Efekti 2 saniye önce aktif et
            particleEffectInstance = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            particleEffectInstance.SetActive(true);
        }

        // 2 saniye bekle
        yield return new WaitForSeconds(2f);

        // Takipçiyi inaktif yap
        gameObject.SetActive(false);

        // Partikül efektini devre dýþý býrak
        if (particleEffectInstance != null)
        {
            particleEffectInstance.SetActive(false);
        }
    }
}
