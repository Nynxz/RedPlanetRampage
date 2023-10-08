using UnityEngine;


// Used for managing the audio, such as volume and for playing audio clips on the player
// Referenced through GameManager.Instance  (singleton)
[RequireComponent(typeof(PlayerManager))]
public class AudioManager : MonoBehaviour {

    //Global Volume
    [SerializeField][Range(0, 1)] private float volume = 0.1f;
    public void SetVolume(float volume) => this.volume = volume;

    // Where to play sounds
    public AudioSource audioSource { get; private set; }

    protected void Start() {
        audioSource = GameManager.Instance.PlayerManager.Player.WeaponSoundSource;
    }

    public void PlayAudioClipOnPlayer(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayAudioClipAtLocation(AudioClip audioClip, Vector3 location) {
        AudioSource.PlayClipAtPoint(audioClip, location);
    }


}
