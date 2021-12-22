using EnemyTankServices;
using GlobalServices;
using UnityEngine;

namespace VFXServices
{
    public class VFXHandler : MonoSingletonGeneric<VFXHandler>
    {
        public void DestroyAllGameObjects()
        {
            DestroyEnemyObjects();
            DestroyGroundObjects();
        }

        private async void DestroyEnemyObjects()
        {
            await new WaitForSeconds(2f);

            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("EnemyTank");

            for (int i = 0; i < enemyObjects.Length; i++)
            {
                enemyObjects[i].GetComponent<EnemyTankView>().tankController.Death();
            }
        }

        private async void DestroyGroundObjects()
        {
            await new WaitForSeconds(3f);

            GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

            for (int i = groundObjects.Length - 1; i >= 0; i--)
            {
                Destroy(groundObjects[i]);
                await new WaitForSeconds(0.05f);
            }
        }
    }

}
