using UnityEngine;
using Managers.Interfaces;

namespace Managers
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour, IGameService
    {
        #region Inspector Fields
        [Header("Background Music")]
        [SerializeField] private AudioClip m_backgroundMusic;
        [SerializeField][Range(0, 1)] private float m_backgroundMusicVolume = 1;
        #endregion Inspector Fields

        #region Fields
        private AudioSource m_backgroundMusicSource;
        #endregion Field
        
        
        #region Methods
        public void PlayAudioClipAtLocation(AudioClip clip, float volume, Vector3 worldPos)
        {
            var audioSource = CreateAudioSource(clip, volume);
            audioSource.transform.position = worldPos;
            audioSource.spatialize = true;
            audioSource.Play();
            
            Destroy(audioSource.gameObject, clip.length);
        }

        public void PlayAudioClip(AudioClip clip, float volume)
        {
            var audioSource = CreateAudioSource(clip, volume);
            audioSource.spatialize = false;
            audioSource.Play();
            
            Destroy(audioSource.gameObject, clip.length);
        }

        private AudioSource CreateAudioSource(AudioClip clip, float volume)
        {
            var soundObject = new GameObject(clip.name);
            soundObject.transform.parent = transform;

            var audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;

            return audioSource;
        }
        #endregion Methods
        
        
        #region GameService Methods
        public void Initialize()
        {
            if (m_backgroundMusic == null)
                return;
            
            m_backgroundMusicSource = GetComponent<AudioSource>();
            m_backgroundMusicSource.clip = m_backgroundMusic;
            m_backgroundMusicSource.volume = m_backgroundMusicVolume;
            m_backgroundMusicSource.loop = true;
            
            m_backgroundMusicSource.Play();
            DontDestroyOnLoad(gameObject);
        }
        #endregion GameService Methods
    }
}