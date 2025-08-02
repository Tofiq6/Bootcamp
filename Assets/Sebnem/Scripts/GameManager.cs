using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isLyraHugged;
    public bool isLyraInBoat;
    public bool haveKey = false;
    public bool swordInHand = false;
    public bool isChestOpened = false;  
    public bool isAllFlowersCollected = false;
    public bool isGameEnd = false;

    private void Awake()
    {
        LoadGame();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde yok olma
        }
        else
        {
            Destroy(gameObject); // Zaten varsa yenisini yok et
        }
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SaveGame()
    {
        if (isLyraHugged)
        {
            PlayerPrefs.SetInt("Hug", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Hug", 0);
        }

        if (isLyraInBoat)
        {
            PlayerPrefs.SetInt("Boat", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Boat", 0);
        }

        if (haveKey)
        {
            PlayerPrefs.SetInt("Key", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Key", 0);
        }

        if (swordInHand)
        {
            PlayerPrefs.SetInt("Sword", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sword", 0);
        }

        
        if (isChestOpened)
        {
            PlayerPrefs.SetInt("Chest", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Chest", 0);
        }

        if (isAllFlowersCollected)
        {
            PlayerPrefs.SetInt("Flower", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Flower", 0);
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("Hug") == 1)
        {
            isLyraHugged = true;
            DynamicTask.Instance.StartTask("PROTECTING THE LYRA", "Preserve Lyra's body in the moonlight cave");
        }
        else
        {
            isLyraHugged = false;
        }

        if (PlayerPrefs.GetInt("Boat") == 1)
        {
            isLyraInBoat = true;
        }
        else
        {
            isLyraInBoat = false;
        }

        if (PlayerPrefs.GetInt("Key") == 1)
        {
            haveKey = true;
        }
        else
        {
            haveKey = false;
        }

        if (PlayerPrefs.GetInt("Sword") == 1)
        {
            swordInHand = true;
        }
        else
        {
            swordInHand = false;
        }

        
        if (PlayerPrefs.GetInt("Chest") == 1)
        {
            isChestOpened = true;
        }
        else
        {
            isChestOpened = false;
        }

        if (PlayerPrefs.GetInt("Flower") == 1)
        {
            isAllFlowersCollected = true;
        }
        else
        {
            isAllFlowersCollected = false;
        }
    }
}
