using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, InputSystem.IPlayerActions
{
    private InputSystem inputSystem;
    public Vector2 movementValue;
    public bool isGround;

    public event Action OnJumpEvent;

    void Start()
    {
        inputSystem = new InputSystem();
        inputSystem.Player.SetCallbacks(this);
        inputSystem.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
        }
    }
}
