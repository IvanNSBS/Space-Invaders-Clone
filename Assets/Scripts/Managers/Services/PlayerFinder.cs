using Player;
using Managers.Interfaces;

namespace Managers.Services
{
    public class PlayerFinder : IGameService
    {
        #region Fields
        private PlayerController m_playerController;
        #endregion Fields

        #region Properties
        public PlayerController PlayerController => m_playerController;
        #endregion Properties
        

        #region Methods
        public void SetPlayerData(PlayerController playerController)
        {
            m_playerController = playerController;
        }
        #endregion Methods
        
        
        #region GameService Methods
        public void Initialize()
        {
        }
        #endregion GameService Methods
    }
}