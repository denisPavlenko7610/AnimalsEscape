using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AnimalsEscape.Music
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private MusicSO _musicSo;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        private void Update()
        {
            if (!_musicSource.isPlaying)
            {
                PlaySource(_musicSource);
            }
        }

        private void PlaySource(AudioSource audioSource)
        {
            audioSource.PlayOneShot(GetRandomMusic());
        }

        private AudioClip GetRandomMusic()
        {
            var number = SetRandomNumber(0, _musicSo.Music.Count);
            return _musicSource.clip = _musicSo.Music[number];
        }

        private int SetRandomNumber(int minIndex, int maxIndex) => Random.Range(minIndex, maxIndex);
    }
}