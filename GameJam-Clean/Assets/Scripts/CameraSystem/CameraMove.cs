using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace CameraSystem
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraPivot;

        [Header("Zoom Settings")] [SerializeField]
        private float _zoomSpeed = 1f;

        [SerializeField] private float _minZoom = 2f;
        [SerializeField] private float _maxZoom = 10f;

        [Header("Move Settings")] [SerializeField]
        private float _moveSpeed = 1f;
        [SerializeField] private Vector2 _minMaxX;
        [SerializeField] private Vector2 _minMaxZ;

        [Header("Pan Settings")]
        [SerializeField]
        private float _xPanSpeed = 1f;
        [SerializeField] private float _yPanSpeed = 1f;

        private ECameraState _cameraState = ECameraState.AllowedToMove;

        private PlayerInputActions _inputActions;
        private InputAction _leftMouseButton;
        private InputAction _rightMouseButton;
        private InputAction _scrollDelta;
        private InputAction _mouseDelta;


        /// <summary>
        /// assign in inspector of the OnStateUpdate event
        /// </summary>
        /// <param name="state">new state</param>
        public void UpdateState(ECameraState state)
        {
            _cameraState = state;
        }

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void Update()
        {
            if (_cameraState != ECameraState.AllowedToMove)
                return;

            Vector2 mouseDelta = _mouseDelta.ReadValue<Vector2>();

            if (_leftMouseButton.IsPressed())
            {
                Move(mouseDelta);
            }

            if (_rightMouseButton.IsPressed())
            {
                Pan(mouseDelta);
            }

            Vector2 scrollDelta = _scrollDelta.ReadValue<Vector2>();
            Zoom(scrollDelta.y);
        }

        private void Pan(Vector2 delta)
        {   
            delta *= _moveSpeed * Time.deltaTime;
            Vector3 rotation = _cameraPivot.rotation.eulerAngles;
            rotation.x = Mathf.Clamp(rotation.x - delta.y * _yPanSpeed, 0, 90);
            rotation.y += delta.x * _xPanSpeed;
            _cameraPivot.rotation = Quaternion.Euler(rotation);
        }

        private void Move(Vector2 delta)
        {
            delta *= _moveSpeed * Time.deltaTime;
            _cameraPivot.transform.position += new Vector3(delta.x, 0, delta.y);
            _cameraPivot.transform.position = new Vector3(
                Mathf.Clamp(_cameraPivot.transform.position.x, _minMaxX.x, _minMaxX.y),
                _cameraPivot.transform.position.y,
                Mathf.Clamp(_cameraPivot.transform.position.z, _minMaxZ.x, _minMaxZ.y));
        }

        private void Zoom(float delta)
        {
            delta *= _zoomSpeed * Time.deltaTime;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - delta, _minZoom, _maxZoom);
        }

        private void OnEnable()
        {
            EnableInput();
        }

        private void OnDisable()
        {
            DisableInput();
        }

        private void EnableInput()
        {
            _leftMouseButton = _inputActions.Player.LMB;
            _rightMouseButton = _inputActions.Player.RMB;
            _scrollDelta = _inputActions.Player.ScrollDelta;
            _mouseDelta = _inputActions.Player.MouseDelta;

            _leftMouseButton.Enable();
            _rightMouseButton.Enable();
            _scrollDelta.Enable();
            _mouseDelta.Enable();
        }

        private void DisableInput()
        {
            _leftMouseButton.Disable();
            _rightMouseButton.Disable();
            _scrollDelta.Disable();
            _mouseDelta.Disable();
        }
    }
}