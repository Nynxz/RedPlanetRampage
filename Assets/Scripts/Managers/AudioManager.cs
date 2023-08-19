using UnityEngine;


// Used for managing the audio, such as volume and for playing audio clips on the player
// Referenced through GameManager.Instance  (singleton)
public class AudioManager : MonoBehaviour {

    //Global Volume
    [SerializeField][Range(0, 1)] private float volume = 0.1f;
    public void SetVolume(float volume) => this.volume = volume;

    // Where to play sounds
    public AudioSource audioSource { get; private set; }

    protected void Start() {
        audioSource = GameManager.Instance.PlayerManager.Player.GetComponent<AudioSource>();
    }

    public void PlayAudioClipOnPlayer(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip, volume);
    }


}
