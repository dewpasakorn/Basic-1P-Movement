using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [Header("Head Rotation")]
    public Transform head;
    public Camera cam;

    [Header("Ground Checker")]
    public LayerMask groundLayer;
    public float groundCheckerDistance = 1.1f;

    [Header("Other Component")]
    public InputReader inputReader;
    public Rigidbody playerRid;

    [Header("Movement Properties")]
    public float jumpHeight = 3;
    public float movementSpeed = 5;
    public float rotationDamping = 15;

    void Start()
    {
        Setup();
        SwitchState(new PlayerMovementState(this));
    }

    void Setup()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
