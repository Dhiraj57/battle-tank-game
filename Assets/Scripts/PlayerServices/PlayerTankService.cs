using GlobalServices;
using System.Collections.Generic;
using TankSO;
using UnityEngine;

namespace PlayerTankServices
{
    public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
    {
        public PlayerTankView playerTankView;

        public TankSOList playerTankList;
        public Joystick rightJoystick;
        public Joystick leftJoystick;

        private PlayerTankController tankController;
        private List<PlayerTankController> playerTanks = new List<PlayerTankController>();
        private TankType playerTankType;

        private void Start()
        {
            playerTankType = (TankType) Random.Range(0, playerTankList.tanks.Length);
        
            tankController = CreatePlayerTank(playerTankType);
            playerTankView = tankController.tankView;           

            SetPlayerTankControlReferences();
        }

        private void Update()
        {
            if (tankController != null)
            {
                tankController.UpdateTankController();
            }
        }

        private void FixedUpdate()
        {
            if(tankController != null)
            {
                tankController.FixedUpdateTankController();
            }
        }

        private PlayerTankController CreatePlayerTank(TankType tanktype)
        {
            foreach (TankScriptableObject tank in playerTankList.tanks)
            {
                if (tank.tankType == playerTankType)
                {
                    PlayerTankModel tankModel = new PlayerTankModel(tank);
                    PlayerTankController tankController = new PlayerTankController(tankModel, playerTankView);
                    playerTanks.Add(tankController);
                    return tankController;
                }
            }
            return null;   
        }

        private void SetPlayerTankControlReferences()
        {
            if(tankController != null)
            {
                tankController.SetJoystickReference(rightJoystick, leftJoystick);
            }
        }

        public PlayerTankController GetTankController(int index = 0) 
        {
            return playerTanks[index];
        }

        public void DestroyTank(PlayerTankController tank)
        {
            for (int i = 0; i < playerTanks.Count; i++)
            {
                if (playerTanks[i] == tank)
                {
                    playerTanks[i] = null;
                    playerTanks.Remove(tank);
                }
            }
        }

        public void TurnONTanks()
        {
            for (int i = 0; i < playerTanks.Count; i++)
            {
                if (playerTanks[i] != null)
                {
                    if (playerTanks[i].tankView)  playerTanks[i].tankView.gameObject.SetActive(true);

                }
            }
        }

        public void TurnOFFTanks()
        {
            for (int i = 0; i < playerTanks.Count; i++)
            {
                if (playerTanks[i] != null)
                {
                    if (playerTanks[i].tankView)  playerTanks[i].tankView.gameObject.SetActive(false);
                }
            }
        }
    }
}
