using UnityEngine;
using Managers.Interfaces;
using System.Collections.Generic;

namespace Managers
{
    public class ServiceLocator
    {
        #region Singleton
        public static ServiceLocator Current
        {
            get;
            private set;
        }
        #endregion Singleton
        
        
        #region Fields
        private readonly Dictionary<string, IGameService> m_services;
        #endregion Fields
        
        
        #region Constructor
        private ServiceLocator()
        {
            m_services = new Dictionary<string, IGameService>();
        }
        #endregion Constructor
        
        
        #region Methods
        public static void Initialize()
        {
            Current = new ServiceLocator();
        }
        
        public T GetService<T>() where T : class, IGameService
        {
            string key = typeof(T).Name;
            if (!m_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                return null;
            }

            return (T)m_services[key];
        }
        
        public void RegisterService<T>(T service) where T : class, IGameService
        {
            string key = typeof(T).Name;
            if (m_services.ContainsKey(key))
            {
                Debug.LogError($"Service <{key}> has been registered already.");
                return;
            }

            service.Initialize();
            m_services.Add(key, service);
        }

        public void UnregisterService<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!m_services.ContainsKey(key))
            {
                Debug.LogError($"Service of type {key} which is not registered.");
                return;
            }

            m_services.Remove(key);
        }
        #endregion Methods
    }
}