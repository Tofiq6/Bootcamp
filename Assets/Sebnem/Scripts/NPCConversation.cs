using System.Collections;
using UnityEngine;
using TMPro; // TextMesh Pro kullanýmý

public class NPCConversation : MonoBehaviour
{
    public Animator animator; // NPC animasyonlarý
    public AudioSource audioSource; // Sesleri çalmak için AudioSource
    public TextMeshProUGUI subtitleText; // Altyazý metni (TextMesh Pro kullanarak)
    public TextMeshProUGUI actionPromptText; // "T tuþuna bas" mesajý (TextMesh Pro kullanarak)

    // Her konuþma için ses, altyazý, npcTalks ve süreler
    [System.Serializable]
    public class DialogueElement
    {
        public AudioClip audio; // Ses dosyasý
        public string subtitle; // Altyazý
        public bool npcTalks; // NPC'nin konuþup konuþmadýðý
        public float subtitleDuration; // Altyazýnýn gösterilme süresi
    }

    public DialogueElement[] dialogues; // Tüm konuþma verileri

    public float interactDistance = 3f; // NPC'ye yaklaþma mesafesi
    public Transform player; // Oyuncunun pozisyonu

    private int currentIndex = 0; // Þu anki konuþma indexi
    private bool isPlayerInRange = false; // Oyuncu NPC'nin yakýnýnda mý?

    void Start()
    {
        // Baþlangýçta idle animasyonunu baþlat
        animator.SetBool("isTalking", false);
        subtitleText.text = ""; // Altyazýyý temizle
        actionPromptText.text = ""; // "T tuþuna bas" mesajýný temizle
    }

    void Update()
    {
        // Oyuncu NPC'ye yaklaþýyor mu?
        float distanceToNPC = Vector3.Distance(player.position, transform.position);
        if (distanceToNPC <= interactDistance)
        {
            if (!isPlayerInRange)
            {
                // Oyuncu yaklaþtýðýnda "T tuþuna bas" mesajýný göster
                actionPromptText.text = "T tuþuna bas";
                isPlayerInRange = true;
            }

            // Eðer T tuþuna basýldýysa konuþmayý baþlat
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartConversation();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                // Oyuncu uzaklaþýnca mesajý gizle
                actionPromptText.text = "";
                isPlayerInRange = false;
            }
        }
    }

    public void StartConversation()
    {
        StartCoroutine(PlayConversation());
    }

    IEnumerator PlayConversation()
    {
        for (currentIndex = 0; currentIndex < dialogues.Length; currentIndex++)
        {
            DialogueElement currentDialogue = dialogues[currentIndex];

            // NPC'nin konuþma olup olmadýðýný kontrol et
            if (currentDialogue.npcTalks)
            {
                // NPC konuþuyor
                animator.SetBool("isTalking", true);
                audioSource.clip = currentDialogue.audio; // NPC sesini seç
                audioSource.Play();
            }
            else
            {
                // Oyuncu konuþuyor
                animator.SetBool("isTalking", true);
                // Burada oyuncu sesini oynatabilirsiniz, ama þimdilik sadece animasyon ile devam ediyoruz
                // Örnek olarak, player sesini farklý bir AudioSource üzerinden çalabilirsiniz.
            }

            // Altyazýyý göster
            subtitleText.text = currentDialogue.subtitle;

            // Altyazýnýn süresi kadar bekle
            yield return new WaitForSeconds(currentDialogue.subtitleDuration);

            // Altyazýyý temizle
            subtitleText.text = "";

            // Konuþma animasyonunu durdur
            animator.SetBool("isTalking", false);

            // Bir sonraki konuþmayý baþlatmadan önce kýsa bir duraklama
            yield return new WaitForSeconds(1f);
        }
    }
}
