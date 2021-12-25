using System.Collections.Generic;
using GlobalServices;
using System;
using UnityEngine;

namespace GameplayServices
{
    public class CameraController : MonoSingletonGeneric<CameraController>
    {
        private List<Transform> targets = new List<Transform>();

        [SerializeField] private float dampTime = 0.2f;
        [SerializeField] private float targetAdditionDampTime = 1.5f;
        [SerializeField] private float endTargetDampTime = 2.1f;
        [SerializeField] private float screenEdgeBuffer = 4f;
        [SerializeField] private float MinSize = 6.5f;
        [SerializeField] private float MaxSize = 10f;

        private Camera camera;
        private float originalDampTime;
        private float zoomSpeed;
        private Vector3 moveVelocity;
        private Vector3 desiredPosition;

        private bool b_IsGameOver;
        private bool b_IsMove;

        protected override void Awake()
        {
            base.Awake();

            camera = GetComponentInChildren<Camera>();
            originalDampTime = dampTime;
            b_IsGameOver = false;
            b_IsMove = true;
        }

        private void OnEnable()
        {
            EventService.Instance.OnGameOver += GameOver;
            EventService.Instance.OnGamePaused += GamePaused;
            EventService.Instance.OnGameResumed += GameResumed;
        }

        private void OnDisable()
        {
            EventService.Instance.OnGameOver -= GameOver;
            EventService.Instance.OnGamePaused -= GamePaused;
            EventService.Instance.OnGameResumed -= GameResumed;
        }

        private void FixedUpdate()
        {
            if(b_IsMove)
            {
                Move();
                Zoom();
            }
            else if(b_IsGameOver)
            {
                SetCameraToGameOverCondition();
            }

            if(dampTime != originalDampTime && !b_IsGameOver && b_IsMove)
            {
                ResetDampTime();
            }
        }

        async private void GameOver()
        {
            b_IsGameOver = true;
            RemoveAllCameraTargetPositions();
            b_IsMove = false;
        }

        private void SetCameraToGameOverCondition()
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(6,8.5f,0), ref moveVelocity, 2f);
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, 20, ref zoomSpeed, 2f);
        }

        private void GamePaused()
        {
            b_IsMove = false;
        }

        private void GameResumed()
        {
            b_IsMove = true;
        }

        private void Move()
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
        }

        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            for(int i = 0; i < targets.Count; i++)
            {
                if(!targets[i].gameObject.activeSelf)
                {
                   continue;
                }

                if(i == 0 && !b_IsGameOver)
                {
                    averagePos += targets[i].position * 3;
                    numTargets += 3;
                }
                else
                {
                    averagePos += targets[i].position;
                    numTargets++;
                }
            }

            if(numTargets > 0)
            {
                averagePos /= numTargets;
            }

            averagePos.y = transform.position.y;
            desiredPosition = averagePos;
        }

        private void Zoom()
        {
            float requiredSize = FindRequiredSize();
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
        }

        private float FindRequiredSize()
        {
            Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);
            float size = 0f;

            for(int i = 0; i < targets.Count; i++)
            {
                if(!targets[i].gameObject.activeSelf)
                {
                    continue;
                }

                Vector3 targetLocalPosition = transform.InverseTransformPoint(targets[i].position);
                Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

                size = Math.Max(size, Mathf.Abs(desiredPositionToTarget.y));
                size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / camera.aspect);
            }

            size = Mathf.Min(size, MaxSize);

            size += screenEdgeBuffer;
            size = Mathf.Max(size, MinSize);

            return size;
        }

        public void SetStartPositionAndSize()
        {
            FindAveragePosition();

            transform.position = desiredPosition;
            camera.orthographicSize = FindRequiredSize();
        }

        async public void AddCameraTargetPosition(Transform target)
        {
            dampTime = targetAdditionDampTime;
            targets.Add(target);
        }

        public void RemoveCameraTargetPosition(Transform target)
        {
            if(!b_IsGameOver)
            {
                dampTime = targetAdditionDampTime;
            }
            targets.Remove(target);
        }

        public void RemoveAllCameraTargetPositions()
        {         
            for (int i = 0; i < targets.Count; i++)
            {
                targets.Remove(targets[i]);
            }
        }

        private void ResetDampTime()
        {
            dampTime = Mathf.Lerp(dampTime, originalDampTime, Time.deltaTime);
        }
    }
}
