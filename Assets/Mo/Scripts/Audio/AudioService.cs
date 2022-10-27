using UnityEngine;

namespace Mo.Audio
{
    public class AudioService : MonoBehaviour
    {
        public AudioSource soundTrackAudioSource;

        [Range(0.0f, 1.0f)]
        public float soundTrackVolume = 1.0f;

        public static AudioService Instance { get; private set; }

        private float previousVolume = 1.0f;
        private float beforeMuteVolume = 1.0f;

        public void SetSoundTrackVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            soundTrackVolume = volume;
            soundTrackAudioSource.volume = volume;
        }

        public float SoundtrackVolume
        {
            get => soundTrackVolume;
            set
            {
                SetSoundTrackVolume(value);
            }
        }

        public void MuteSoundtrack(bool mute)
        {
            if (mute)
            {
                beforeMuteVolume = soundTrackAudioSource.volume;
                soundTrackVolume = 0.0f;
            }
            else
            {
                soundTrackVolume = beforeMuteVolume;
            }

            soundTrackAudioSource.mute = mute;
        }

        private void Update()
        {
            if (previousVolume != soundTrackVolume)
            {
                previousVolume = soundTrackVolume;
                SetSoundTrackVolume(soundTrackVolume);
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }
}