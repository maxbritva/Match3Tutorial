using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [Header("AudioSources")] 
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _pitchSoundSource;
        [SerializeField] private AudioSource _normalSoundSource;
        [SerializeField] private AudioMixer _audioMixer;

        [Header("AudioClips")] 
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _deselect;
        [SerializeField] private AudioClip _match;
        [SerializeField] private AudioClip _noMatch;
        [SerializeField] private AudioClip _whoosh;
        [SerializeField] private AudioClip _pop;
        [SerializeField] private AudioClip _stopMusic;
        [SerializeField] private AudioClip _remove;
        [SerializeField] private AudioClip _win;
        [SerializeField] private AudioClip _loose;
        
        // gameData
        private bool _isEnabledSound = true;

        public void PlayStopMusic() => PlayNormalPitch(_stopMusic);
        public void PlayClick() => PlayNormalPitch(_click);
        public void PlayDeselect() => PlayNormalPitch(_deselect);
        public void PlayMatch() => PlayNormalPitch(_match);
        public void PlayNoMatch() => PlayNormalPitch(_noMatch);
        public void PlayWin() => PlayNormalPitch(_win);
        public void PlayLoose() => PlayNormalPitch(_loose);
        public void PlayWhoosh() => PlayRandomPitch(_whoosh);
        public void PlayPop() => PlayRandomPitch(_pop);
        public void PlayRemove() => PlayRandomPitch(_remove);

        public void SetSoundVolume()
        {
            if (_isEnabledSound)
            {
                _audioMixer.SetFloat("Volume", -6f);
                _musicSource.Play();
            }
            else
            {
                _audioMixer.SetFloat("Volume", -80f);
                _musicSource.Stop();
            }
        }

        public void PlayGameMusic()
        {
            StopMusic();
            _musicSource.clip = _gameMusic;
            SetSoundVolume();
        }
        
        public void PlayMenuMusic()
        {
            StopMusic();
            _musicSource.clip = _menuMusic;
            SetSoundVolume();
        }
        
        public void StopMusic() => _musicSource.Stop();

        private void PlayRandomPitch(AudioClip clip)
        {
            _pitchSoundSource.pitch = Random.Range(0.8f, 1.2f);
            _pitchSoundSource.PlayOneShot(clip);
        }
        
        private void PlayNormalPitch(AudioClip clip) =>  
            _normalSoundSource.PlayOneShot(clip);
    }
}