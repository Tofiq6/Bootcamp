using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ambientSource;
    public AudioSource zoneSource;

    public AudioClip ambientClip;
    public AudioClip caveClip;
    public AudioClip castleClip;

    private int zoneCount = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayAmbient();
    }

    public void PlayAmbient()
    {
        if (!ambientSource.isPlaying)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }

    public void StopAmbient()
    {
        if (ambientSource.isPlaying)
            ambientSource.Stop();
    }

    public void EnterZone(AudioClip zoneClip)
    {
        zoneCount++;

        StopAmbient();

        if (zoneSource.clip != zoneClip || !zoneSource.isPlaying)
        {
            zoneSource.clip = zoneClip;
            zoneSource.loop = true;
            zoneSource.Play();
        }
    }

    public void ExitZone()
    {
        zoneCount--;

        if (zoneCount <= 0)
        {
            zoneCount = 0;
            zoneSource.Stop();
            PlayAmbient();
        }
    }
}
