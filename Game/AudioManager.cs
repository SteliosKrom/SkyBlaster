using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null)
        {
            if (source.enabled)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Audio source is disabled");
            }
        }
    }
}

