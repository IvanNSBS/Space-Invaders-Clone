using DG.Tweening;
using Managers.Services;
using UnityEngine;

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
            
            DOTween.SetTweensCapacity(300, 200);
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        private void InitializeServices()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.RegisterService(new ScoreManager());
            ServiceLocator.Current.RegisterService(new PlayerFinder());
            
            // ServiceLocator.Current.RegisterService(new SoundManager());
        }
        #endregion Methods
    }
}