using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AnimalsEscape.Music
{
    public class Music : MonoBehaviour
    {
        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] MusicSO _musicSo;
        [SerializeField] AudioSource _musicSource;
        [SerializeField] AudioSource _sfxSource;
        [SerializeField] Image _soundImage;
        [SerializeField] Button _soundButton;

        bool isStop;
        const string key = "isStop";

        private void OnEnable()
        {
            LoadVolumeSettings();
            _soundButton.onClick.AddListener(StopPlayingSound);
        }

        private void LoadVolumeSettings()
        {
            if (PlayerPrefs.HasKey(key))
            {
                isStop = PlayerPrefs.GetInt(key) == 1;
                SetSoundSettings();
            }
        }

        private void OnDisable()
        {
            _soundButton.onClick.RemoveListener(StopPlayingSound);
        }

        void Update()
        {
            CheckPlayMusic();
        }

        private void CheckPlayMusic()
        {
            if (!_musicSource.isPlaying && !isStop)
            {
                PlaySource(_musicSource);
            }
        }

        void PlaySource(AudioSource audioSource)
        {
            audioSource.PlayOneShot(GetRandomMusic());
        }

        void StopPlayingSound()
        {
            isStop = !isStop;
            SetSoundSettings();
        }

        private void SetSoundSettings()
        {
            if (isStop)
                MuteVolume();
            else
                UnmuteVolume();
        }

        private void UnmuteVolume()
        {
            PlayerPrefs.SetInt(key, 0);
            var standardVolume = -11f;
            _audioMixer.SetFloat("MasterVolume", standardVolume);
            _soundImage.sprite = Resources.Load<Sprite>("Sound/VolumeUnmute");
        }

        private void MuteVolume()
        {
            PlayerPrefs.SetInt(key, 1);
            var muteVolume = -80f;
            _audioMixer.SetFloat("MasterVolume", muteVolume);
            _soundImage.sprite = Resources.Load<Sprite>("Sound/VolumeMute");
        }

        AudioClip GetRandomMusic()
        {
            var number = SetRandomNumber(0, _musicSo.Music.Count);
            return _musicSource.clip = _musicSo.Music[number];
        }

        int SetRandomNumber(int minIndex, int maxIndex) => Random.Range(minIndex, maxIndex);
    }
}