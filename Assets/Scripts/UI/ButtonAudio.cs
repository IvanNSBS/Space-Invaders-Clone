using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    #pragma warning disable CS0649
    [RequireComponent(typeof(Button))]
    public class ButtonAudio : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private AudioClip m_buttonSoundClip;
        [SerializeField][Range(0,1)] private float m_soundVolume = 1;
        #endregion Inspector Fields

        
        #region MonoBehaviour Methods
        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(PlayButtonSound);
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void PlayButtonSound()
        {
            if (m_buttonSoundClip == null)
                return;
            
            ServiceLocator.Current.GetService<SoundManager>().PlayAudioClip(m_buttonSoundClip, m_soundVolume);
        }
        #endregion Methos
    }
}