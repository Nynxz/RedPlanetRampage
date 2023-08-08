using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    private StarterInputActions inputActions;
    
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool sprintInput { get; private set; }
    public bool interactInput { get; private set; }

    private bool cursorLocked = true;


    public void SetJump(bool val) => jumpInput = val;
    public void SetInteract(bool val) => interactInput = val;

    void Start() {

        inputActions = new StarterInputActions();
        inputActions.Enable();

        inputActions.Player.Move.performed += (ctx) => { moveInput = ctx.ReadValue<Vector2>(); };
        inputActions.Player.Move.canceled += (ctx) => { moveInput = Vector2.zero; };

        inputActions.Player.Look.performed += (ctx) => { lookInput = ctx.ReadValue<Vector2>(); };
        inputActions.Player.Look.canceled += (ctx) => { lookInput = Vector2.zero; };

        inputActions.Player.Jump.started += (ctx) => { jumpInput = true; };
        inputActions.Player.Jump.canceled += (ctx) => { jumpInput = false; };

        inputActions.Player.Sprint.started += (ctx) => { sprintInput = true; };
        inputActions.Player.Sprint.canceled += (ctx) => { sprintInput = false; };

        inputActions.Player.Interact.started += (ctx) => { interactInput = true; };
        inputActions.Player.Interact.canceled += (ctx) => { interactInput = false; };

    }

    private void OnApplicationFocus(bool hasFocus) {
        SetCursorState(cursorLocked);
    }
    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
