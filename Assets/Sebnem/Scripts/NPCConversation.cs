using UnityEngine;
using TMPro;
using System.Collections;

public class NPCConversation : MonoBehaviour, IInteractable
{
    public Animator animator; // NPC animasyonlarý
    public AudioSource audioSource; // Sesleri çalmak için AudioSource
    public TextMeshProUGUI subtitleText; // Altyazý metni (TextMesh Pro kullanarak)
    public TextMeshProUGUI actionPromptText; // "T tuþuna bas" mesajý (TextMesh Pro kullanarak)
    public TextMeshProUGUI efsunText;
    public DynamicChaseTrigger trigger;
    public bool Bilge = false;
    public bool Kaptan = false;
    public bool Amara = false;
    public bool Fiona = false;

    [System.Serializable]
    public class DialogueElement
    {
        public AudioClip audio; // Ses dosyasý
        public string subtitle; // Altyazý
        public bool npcTalks; // NPC'nin konuþup konuþmadýðý
        public float subtitleDuration; // Altyazýnýn gösterilme süresi
    }

    public DialogueElement[] dialogues; // Tüm konuþma verileri

    private int currentIndex = 0; // Þu anki konuþma indexi

    void Start()
    {
        if (trigger != null)
        {
            trigger.gameObject.SetActive(false);
        }
    }

    public string GetDescription()
    {
        return "Press T to talk"; // UI'de görünen açýklama
    }

    public void Interact()
    {
        StartConversation(); // Etkileþimde NPC konuþmasýný baþlat
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
                animator.SetBool("isTalking", true);                
            }
            else
            {
                animator.SetBool("isTalking", true); // NPC deðilse, animasyon oynatýyoruz.                
            }

            audioSource.clip = currentDialogue.audio; // NPC sesini seç
            audioSource.Play();

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

        if (Bilge)
        {
            DynamicTask.Instance.StartTask("TAVERN DIARIES", "Find the captain in the tavern");
        }

        if (Amara)
        {
            DynamicTask.Instance.StartTask("DARK DUNGEON", "go to the dungeon");
            trigger.gameObject.SetActive(true);
            trigger.FollowAndTalk();
            efsunText.text = "Efsun elde edildi";
            yield return new WaitForSeconds(5f);
            efsunText.text = "";
            DynamicTask.Instance.dynamicChase.FollowAndTalk();
        }

        if (Kaptan)
        {
            DynamicTask.Instance.StartTask("LOST SWORD", "Find the map pieces in the castle");
        }

        if (Fiona && trigger != null)
        {
            trigger.gameObject.SetActive(true);
        }
    }
}
