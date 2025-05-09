using UnityEngine;

//for looping sounds, like bg music

public class SecondarySoundManager : MonoBehaviour
{
    [HideInInspector] public static SecondarySoundManager secondarySoundManagerInstance {get; private set;}

    public AudioSource backgroundMusic;

    private void Awake() 
    {
        secondarySoundManagerInstance = this;
    }

    public void BackgroundSoundPlayer(bool state)
    {
        backgroundMusic.enabled = state;
    }
}
