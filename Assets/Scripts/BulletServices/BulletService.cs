using BulletSO;
using GlobalServices;
using UnityEngine;

namespace BulletServices
{ 
    public class BulletService : MonoSingletonGeneric<BulletService>
    {
        public BulletSOList bulletList;
        private bool b_IsPaused;

        private void Start()
        {
            b_IsPaused = false;
            EventService.Instance.OnGamePaused += GamePaused;
            EventService.Instance.OnGameResumed += GameResumed;
        }

        private void OnDisable()
        {
            EventService.Instance.OnGamePaused -= GamePaused;
            EventService.Instance.OnGameResumed -= GameResumed;
        }

        private void GamePaused()
        {
            b_IsPaused = true;
        }

        private void GameResumed()
        {
            b_IsPaused = false;
        }

        public BulletController FireBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
        {
            if(!b_IsPaused)
            {
                return CreateBullet(bulletType, bulletTransform, launchForce);
            }
            return null;
        }

        private BulletController CreateBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
        {
            foreach (BulletScriptableObject bullet in bulletList.bulletTypes)
            {
                if(bullet.bulletType == bulletType)
                {
                    BulletModel bulletModel = new BulletModel(bulletList.bulletTypes[(int)bulletType].damage, 
                                                              bulletList.bulletTypes[(int)bulletType].maxLifeTime,
                                                              bulletList.bulletTypes[(int)bulletType].explosionRadius,
                                                              bulletList.bulletTypes[(int)bulletType].explosionForce);

                    BulletController bulletController = new BulletController(bulletModel, bulletList.bulletTypes[(int)bulletType].bulletView, bulletTransform, launchForce);
                    return bulletController;
                }
            }
            return null;
        }
    }
}

