using GameplayServices;
using GlobalServices;
using UnityEngine;

namespace EnemyTankServices
{
    public class EnemyTankController
    {
        public EnemyTankModel tankModel { get; }
        public EnemyTankView tankView { get; }
     
        public EnemyTankController(EnemyTankModel tankModel, EnemyTankView tankPrefab)
        {
            this.tankModel = tankModel;
            Transform tranform = SpawnPointService.Instance.GetRandomSpawnPoint();
            tankView = GameObject.Instantiate<EnemyTankView>(tankPrefab, tranform.position, tranform.rotation);
            tankView.tankController = this;
        }

        public void UpdateTankController()
        {
            tankModel.b_PlayerInSightRange = Physics.CheckSphere(tankView.transform.position, tankModel.patrollingRange, tankView.playerLayerMask);
            tankModel.b_PlayerInAttackRange = Physics.CheckSphere(tankView.transform.position, tankModel.attackRange, tankView.playerLayerMask);
        }

        public void TakeDamage(int damage)
        {
            tankModel.health -= damage;
            SetHealthUI();
            ShowHealthUI();

            if (tankModel.health <= 0 && !tankModel.b_IsDead)
            {
                Death();
            }
        }

        public void SetHealthUI()
        {
            tankView.healthSlider.value = tankModel.health;
            tankView.fillImage.color = Color.Lerp(tankModel.zeroHealthColor, tankModel.fullHealthColor, tankModel.health / tankModel.maxHealth);
        }

        async public void ShowHealthUI()
        {
            if (tankView)
            {
                tankView.healthSlider.gameObject.SetActive(true);
            }

            await new WaitForSeconds(3f);

            if (tankView)
            {
                tankView.healthSlider.gameObject.SetActive(false);
            }
        }

        public void Death()
        {
            tankModel.b_IsDead = true;

            tankView.explosionParticles.transform.position = tankView.transform.position;
            tankView.explosionParticles.gameObject.SetActive(true);
            tankView.explosionParticles.Play();
            tankView.explosionSound.Play(); 

            tankView.Death();
            EnemyTankService.Instance.DestroyEnemy(this);

            EventService.Instance.InvokeOnEnemyDeathEvent();
        }
    }
}
