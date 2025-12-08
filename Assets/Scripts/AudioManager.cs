using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    // sounds/songs
    [Header("Audio clip")]
    public AudioClip Main_Menu_Theme;

    // audio plays when game starts
    private void Start()
    {
        musicSource.clip = Main_Menu_Theme;
        musicSource.Play();
    }
}
