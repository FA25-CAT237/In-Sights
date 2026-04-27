using UnityEngine;
using UnityEngine.Rendering;
// with help from this tutorial: https://www.youtube.com/watch?v=DU7cgVsU2rM

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    [SerializeField] private AudioSource soundEffectObject;

    public AudioClip music;

    private GameObject player;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (player == null)
            Debug.Log("Player's missing.");
    }

    private void Start()
    {
        // play the music
        if (music != null)
        {
            AudioSource musicSource = Instantiate(soundEffectObject, player.transform.position, Quaternion.identity);
            musicSource.clip = music;
            musicSource.volume = 0.5f;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
            Debug.Log("Music's missing.");
    }

    public void PlaySoundEffectClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawn gameObject
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        // assign audioClip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // get clip length
        float clipLength = audioSource.clip.length;

        // destroy clip after done
        Destroy(audioSource.gameObject, clipLength);
    }
}
