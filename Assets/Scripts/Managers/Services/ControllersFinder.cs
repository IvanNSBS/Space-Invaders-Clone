using Invaders;
using Player;
using Managers.Interfaces;

namespace Managers.Services
{
    public class ControllersFinder : IGameService
    {
        #region Fields
        private PlayerController m_playerController;
        private InvadersController m_invadersController;
        #endregion Fields

        #region Properties
        public PlayerController PlayerController => m_playerController;
        public InvadersController InvadersController => m_invadersController;
        #endregion Properties
        

        #region Methods
        public void SetPlayerData(PlayerController playerController)
        {
            m_playerController = playerController;
        }
        
        public void SetInvadersData(InvadersController invadersController)
        {
            m_invadersController = invadersController;
        }
        #endregion Methods
        
        
        #region GameService Methods
        public void Initialize()
        {
        }
        #endregion GameService Methods
    }
}