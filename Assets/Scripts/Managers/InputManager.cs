using System;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private StarterInputActions inputActions;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool sprintInput { get; private set; }
    public bool interactInput { get; private set; }
    public bool shootInput { get; private set; }
    public bool optionsInput { get; private set; }

    private bool cursorLocked = true;


    public void SetJump(bool val) => jumpInput = val;
    public void SetInteract(bool val) => interactInput = val;

    public Action OptionsPressed;

    protected void Start() {

        inputActions = new StarterInputActions();
        inputActions.Enable();

        inputActions.Player.Move.performed += (ctx) => { moveInput = ctx.ReadValue<Vector2>(); };
        inputActions.Player.Move.canceled += (ctx) => { moveInput = Vector2.zero; };

        inputActions.Player.Look.performed += (ctx) => { lookInput = ctx.ReadValue<Vector2>(); };
        inputActions.Player.Look.canceled += (ctx) => { lookInput = Vector2.zero; };

        inputActions.Player.Jump.started += (ctx) => { jumpInput = true; };
        inputActions.Player.Jump.canceled += (ctx) => { jumpInput = false; };

        inputActions.Player.Sprint.performed += (ctx) => { sprintInput = true; };
        inputActions.Player.Sprint.canceled += (ctx) => { sprintInput = false; };

        inputActions.Player.Interact.started += (ctx) => { interactInput = true; };
        inputActions.Player.Interact.canceled += (ctx) => { interactInput = false; };

        inputActions.Player.Shoot.started += (ctx) => { shootInput = true; };
        inputActions.Player.Shoot.canceled += (ctx) => { shootInput = false; };

        inputActions.Menu.Options.started += (ctx) => { optionsInput = true; OptionsPressed?.Invoke(); };
        inputActions.Menu.Options.canceled += (ctx) => { optionsInput = false; };
    }

    public void DisableInput() {
        inputActions.Player.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void EnableInput() {
        inputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;

    }

    protected void OnApplicationFocus(bool hasFocus) {
        SetCursorState(cursorLocked);
    }
    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
