using UnityEngine;
using System.Collections.Generic;

public enum SFX
{
    Ping,
    ChangeLang,
    ChangeText,
    ClockTik,
    EndCountdownShort
    // Add more sound effect types here
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public SFX soundType;
        public AudioClip clip;
    }

    [Header("Sound Effects")]
    [SerializeField] List<Sound> sounds = new List<Sound>();

    private Dictionary<SFX, AudioClip> soundDict = new Dictionary<SFX, AudioClip>();
    private AudioSource sfxSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            sfxSource = gameObject.AddComponent<AudioSource>();

            // Build dictionary for fast lookup
            foreach (var s in sounds)
            {
                if (!soundDict.ContainsKey(s.soundType))
                    soundDict.Add(s.soundType, s.clip);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(SFX soundType, float volume = 1f)
    {
        if (soundDict.TryGetValue(soundType, out AudioClip clip))
        {
            Debug.Log("Playing sound: " + soundType + ", clip: " + clip.name);
            sfxSource.PlayOneShot(clip,volume);
           

        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundType.ToString());
        }
    }


    public void StopSFX()
    {
      //if (sfxSource.isPlaying)
           // sfxSource.Stop();
    }

}
