using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        public UnityEvent<ECameraState> OnStateUpdate;

        [SerializeField] private Camera _camera;

        private ECameraState _currentState;

        private PlayerInputActions _inputActions;
        private InputAction _leftMouseButton;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
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
            _leftMouseButton.Enable();
            _leftMouseButton.performed += CheckForRobot;
        }

        private void DisableInput()
        {
            _leftMouseButton.Disable();
            _leftMouseButton.performed -= CheckForRobot;
        }

        private void CheckForRobot(InputAction.CallbackContext obj)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
            OnStateUpdate?.Invoke(hit.collider.GetComponent<IRobot>() != null
                ? ECameraState.AllowedToMove
                : ECameraState.NotAllowedToMove);
            
        }
    }
}