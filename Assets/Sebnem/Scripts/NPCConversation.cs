using UnityEngine;
using TMPro;
using System.Collections;

public class NPCConversation : MonoBehaviour, IInteractable
{

    public Animator animator; // NPC animasyonlarý
    public AudioSource audioSource; // Sesleri çalmak için AudioSource
    public TextMeshProUGUI subtitleText; // Altyazý metni (TextMesh Pro kullanarak)
    public TextMeshProUGUI actionPromptText; // "T tuþuna bas" mesajý (TextMesh Pro kullanarak)

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
                audioSource.clip = currentDialogue.audio; // NPC sesini seç
                audioSource.Play();
            }
            else
            {
                animator.SetBool("isTalking", true); // NPC deðilse, animasyon oynatýyoruz.
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
