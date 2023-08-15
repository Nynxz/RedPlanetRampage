using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField][Range(0, 1)] private float volume = 0.1f;

    public AudioSource audioSource { get; private set; }

    void Start() {
        audioSource = GameManager.Instance.PlayerManager.GetPlayer.GetComponent<AudioSource>();
    }

    public void PlayAudioClipOnPlayer(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void SetVolume(float volume) => this.volume = volume;

}
