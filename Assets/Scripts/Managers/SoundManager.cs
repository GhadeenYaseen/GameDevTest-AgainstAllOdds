using UnityEngine;
using System;

/*
    this sound manager allows the playing of a random sound from a category.
    scaleable, and randomness improves UX for frequent sounds
*/

public enum SoundType
{
    UI,
    Hurt,
    Shoot,
    Slam,
    Fire, 
    Explode,
    Equip
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundsList;

    private static SoundManager soundManagerInstance;

    private AudioSource _audioSource;
    private static bool _enableSFX = true;

    private void Awake() 
    {
        soundManagerInstance = this;
        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    //naming array elements of soundsList array in inspector
#if UNITY_EDITOR
    private void OnEnable() 
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundsList, names.Length);

        for(int i = 0 ; i < soundsList.Length ; ++i)
        {
            soundsList[i].name = names[i];
        }
    }
#endif

    public static void PlaySound(SoundType sound, float volume=1)
    {
        if (!_enableSFX) return;

        AudioClip[] clips = soundManagerInstance.soundsList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        soundManagerInstance._audioSource.PlayOneShot(randomClip, volume);
    }

    public void SFX(bool sfxState)
    {
        _enableSFX = sfxState;
    }

    public void UIButton()
    {
        PlaySound(SoundType.UI);
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds {get => sounds;}
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
