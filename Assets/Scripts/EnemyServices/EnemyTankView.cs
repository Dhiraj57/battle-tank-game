using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using PlayerTankServices;
using GlobalServices;
using GameplayServices;
using System.Collections.Generic;

namespace EnemyTankServices
{
    public class EnemyTankView : MonoBehaviour, IDamagable
    {
        public NavMeshAgent navAgent;
        public Transform turret;

        public Transform fireTransform;
        public LayerMask playerLayerMask;
        public LayerMask groundLayerMask;
        public GameObject explosionEffectPrefab;

        public EnemyPatrollingState patrollingState;
        public EnemyChasingState chasingState;
        public EnemyAttackingState attackingState;

        [SerializeField] private EnemyState initialState;
        [HideInInspector] public EnemyState activeState;
        [HideInInspector] public EnemyStates currentState;

        // To display health.
        public Slider healthSlider;
        public Image fillImage;

        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public EnemyTankController tankController;

        [HideInInspector] public AudioSource explosionSound;
        [HideInInspector] public ParticleSystem explosionParticles;

        public AudioSource shootingAudio;
        public AudioClip fireClip;

        private void Awake()
        {
            explosionParticles = Instantiate(explosionEffectPrefab).GetComponent<ParticleSystem>();
            explosionSound = explosionParticles.GetComponent<AudioSource>();
            explosionParticles.gameObject.SetActive(false);
        }

        private void Start()
        {
            tankController.SetHealthUI();
        
            if(PlayerTankService.Instance.playerTankView)
            {
                playerTransform = PlayerTankService.Instance.playerTankView.transform;
            }

            navAgent = GetComponent<NavMeshAgent>();         
            SetEnemyTankColor();
            InitializeState();

            CameraController.Instance.AddCameraTargetPosition(this.transform);
        }

        private void FixedUpdate()
        {
            tankController.UpdateTankController();
        }

        private void InitializeState()
        {
            switch (initialState)
            {
                case EnemyState.Attacking:
                    {
                        currentState = attackingState;
                        break;
                    }
                case EnemyState.Chasing:
                    {
                        currentState = chasingState;
                        break;
                    }
                case EnemyState.Patrolling:
                    {
                        currentState = patrollingState;
                        break;
                    }
                default:
                    {
                        currentState = null;
                        break;
                    }
            }
            currentState.OnStateEnter();
        }

        public float GetRandomLaunchForce()
        {
            return Random.Range(tankController.tankModel.minLaunchForce, tankController.tankModel.maxLaunchForce);
        }

        public void TakeDamage(int damage)
        {
            tankController.TakeDamage(damage);
        }

        public void Death()
        {
            CameraController.Instance.RemoveCameraTargetPosition(this.transform);
            Destroy(gameObject);
        }

        public void SetEnemyTankColor()
        {
            MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankController.tankModel.tankColor;
            }
        }
    }
}
