using System;
using System.Collections.Generic;
using CameraSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PathDrawer : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Queue<Vector3>, UnityAction> _onPathCreated;
        [SerializeField] private Vector3 _cellSize = new(1, 1, 1);
        [SerializeField] private Vector3 _cellOffset = new(0, .5f, 0);
        [SerializeField] private LineRenderer _pathRenderer;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _walkableLayer;

        [SerializeField] private int _gizmoCount = 20;

        private readonly Queue<Vector3> _path = new();
        private Vector3Int _lastCell;

        private ECameraState _state = ECameraState.MoveCamera;
        private PlayerInputActions _inputActions;
        private InputAction _leftMouseButton;

        public void UpdateState(ECameraState state)
        {
            _state = state;
        }

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void EnableInput()
        {
            _leftMouseButton = _inputActions.Player.LMB;
            _leftMouseButton.Enable();
            _leftMouseButton.canceled += CallOnPathCreated;
        }

        private void DisableInput()
        {
            _leftMouseButton.Disable();
            _leftMouseButton.canceled -= CallOnPathCreated;
        }

        private void OnEnable()
        {
            EnableInput();
        }

        private void OnDisable()
        {
            DisableInput();
        }

        private void Update()
        {
            if (_state != ECameraState.MoveRobot)
                return;
            if (!_leftMouseButton.IsPressed())
                return;

            Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(mouseRay, out RaycastHit hit, 1000f, _walkableLayer))
                return;
            Vector3Int cell = GetCellThatContainsPosition(hit.point);

            if (_lastCell == cell)
            {
                Debug.Log("Position is the same as last position");
                return;
            }
            
            _lastCell = cell;
            _path.Enqueue(GetPositionFromCell(cell));
        }

        private void LateUpdate()
        {
            _pathRenderer.positionCount = _path.Count;
            _pathRenderer.SetPositions(_path.ToArray());
        }


        private Vector3Int GetCellThatContainsPosition(Vector3 position)
        {
            int cellX = Mathf.FloorToInt(position.x / _cellSize.x);
            int cellZ = Mathf.FloorToInt(position.z / _cellSize.z);
            return new Vector3Int(cellX, 0, cellZ);
        }

        private Vector3 GetPositionFromCell(Vector3Int cell)
        {
            return new Vector3(cell.x * _cellSize.x + _cellSize.x / 2 + _cellOffset.x,
                cell.y * _cellSize.y + _cellOffset.y, cell.z * _cellSize.z + _cellSize.z / 2 + _cellOffset.z);
        }

        private void CallOnPathCreated(InputAction.CallbackContext ctx)
        {
            if (_state != ECameraState.MoveRobot)
                return;
            _onPathCreated?.Invoke(_path, ClearPath);
        }

        private void ClearPath()
        {
            Debug.Log("Clearing path");
            _pathRenderer.positionCount = 0;
            _path.Clear();
        }
    }
}