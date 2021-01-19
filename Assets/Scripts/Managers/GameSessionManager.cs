using UnityEngine;
using DG.Tweening;
using Managers.Services;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameSessionManager : MonoBehaviour
    {
        #region Singleton
        private static GameSessionManager instance;
        public static GameSessionManager Instance => instance;
        #endregion Singleton

        #region Fields

        #endregion Fields

        
        #region MonoBehaviour Methods
        private void Awake()
        {
            if(instance != null)
                Destroy(gameObject);
            
            instance = this;
            InitializeServices();
            DontDestroyOnLoad(this);
            
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            
            DOTween.SetTweensCapacity(300, 200);
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public void EndSession(float finishDelay)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(finishDelay);
            sequence.AppendCallback(() =>
            {
                SceneManager.LoadSceneAsync("Menu");
            });
        }
        
        private void InitializeServices()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.RegisterService(new ScoreManager());
            ServiceLocator.Current.RegisterService(new ControllersFinder());
            
            GameObject soundManagerPrefab = Instantiate(Resources.Load("Audio/SoundManager")) as GameObject;
            if (soundManagerPrefab != null)
            {
                SoundManager soundManager = soundManagerPrefab.GetComponent<SoundManager>();
                ServiceLocator.Current.RegisterService(soundManager);
            }
        }
        #endregion Methods
    }
}