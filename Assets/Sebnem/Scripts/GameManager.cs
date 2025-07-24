using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isLyraHugged;
    public bool isLyraInBoat;

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
    }

    public void LoadGame()
    {
        if(PlayerPrefs.GetInt("Hug")== 1)
        {
            isLyraHugged=true;
        }
        else
        {
            isLyraHugged=false;
        }

        if (PlayerPrefs.GetInt("Boat") == 1)
        {
            isLyraInBoat = true;
        }
        else
        {
            isLyraInBoat=false;
        }
    }

}
