using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPControllerspace
{
    public class FPInputManager : MonoBehaviour
    {
        public Vector2 MovementInput;
        public Vector2 LookInput;
        public bool SprintInput;
        public bool crouchInput;
        public bool JumpInput=false;
        public bool Actioninput;
        public event Action OnJumpPressed;
        #region Serialized Fields
        [Header("Input Actions")]
        [SerializeField] private InputActionAsset inputActions;
        #endregion

        // These are the actions that are defined in the input actions asset
        public InputAction moveAction { get; private set; }
        public InputAction lookAction { get; private set; }
        public InputAction jumpAction { get; private set; }
        public InputAction sprintAction { get; private set; }
        public InputAction crouchAction { get; private set; }
        public InputAction Action { get; private set; }
        private void Awake()
        {
            JumpInput = false;
            // Get the actions from the input actions asset
            moveAction = inputActions.FindActionMap("PlayerControls").FindAction("Movement");
            lookAction = inputActions.FindActionMap("PlayerControls").FindAction("Look");
            jumpAction = inputActions.FindActionMap("PlayerControls").FindAction("Jump");
            sprintAction = inputActions.FindActionMap("PlayerControls").FindAction("Sprint");
            crouchAction = inputActions.FindActionMap("PlayerControls").FindAction("Crouching");
            Action = inputActions.FindActionMap("PlayerControls").FindAction("Action");

            moveAction.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += ctx => MovementInput = Vector2.zero;

            lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            lookAction.canceled += ctx => LookInput = Vector2.zero;

            sprintAction.performed += ctx => { SprintInput = true; Debug.Log("Sprinting"); };
            sprintAction.canceled += ctx => SprintInput = false;

            jumpAction.performed += ctx => { JumpInput = true; Debug.Log("Jumping"); };
            jumpAction.canceled += ctx => JumpInput = false;

            crouchAction.performed += ctx => { crouchInput = true; Debug.Log("Crouching"); };
            crouchAction.canceled += ctx => crouchInput = false;

            Action.performed += ctx => { Actioninput = true; Debug.Log("Interacting"); };
            Action.canceled += ctx => Actioninput = false;
        }
        
        private void OnJump(bool JumpInput)
        {
           
            if (JumpInput)
            {
               OnJumpPressed?.Invoke();
              
            }
                
        }
        private void Update()
        {
            OnJump(JumpInput);
        }
        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
            crouchAction.Enable();
            Action.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
            crouchAction.Disable();
            Action.Disable();
        }
    }
    }
