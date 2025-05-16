using System;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    public PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }



    #region  Movement

    protected void MovementHandle(float deltaTime)
    {
        Move();
        Rotate(deltaTime);
        GroundCheck();
    }

    protected void Jump()
    {
        if (!stateMachine.inputReader.isGround) { return; }

        float jumpDistance = Mathf.Sqrt(2 * 9.81f * stateMachine.jumpHeight);
        stateMachine.playerRid.AddForce(Vector3.up * jumpDistance, ForceMode.Impulse);
        stateMachine.inputReader.isGround = false;
    }

    protected void GroundCheck()
    {
        Debug.DrawRay(stateMachine.transform.position, -stateMachine.transform.up * stateMachine.groundCheckerDistance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(stateMachine.transform.position, -stateMachine.transform.up, out hit, stateMachine.groundCheckerDistance, stateMachine.groundLayer))
        {
            stateMachine.inputReader.isGround = true;
        }
        else
        {
            stateMachine.inputReader.isGround = false;
        }
    }

    protected void Rotate(float deltaTime)
    {
        Vector3 cameraForward = stateMachine.cam.transform.forward;

        Quaternion headTargetRotation = Quaternion.LookRotation(cameraForward);
        stateMachine.head.rotation = Quaternion.Lerp(stateMachine.head.rotation, headTargetRotation, stateMachine.rotationDamping * deltaTime);

        cameraForward.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, targetRotation, stateMachine.rotationDamping * deltaTime);
    }
    protected void Move()
    {
        Vector3 movement = CalculationMovement() * stateMachine.movementSpeed;
        Vector3 currentVelocity = stateMachine.playerRid.linearVelocity;
        movement.y = currentVelocity.y;

        stateMachine.playerRid.linearVelocity = movement;
    }

    protected Vector3 CalculationMovement()
    {
        Vector3 forward = stateMachine.cam.transform.forward;
        Vector3 right = stateMachine.cam.transform.right;
        forward.y = 0;
        right.y = 0;
        return forward * stateMachine.inputReader.movementValue.y + right * stateMachine.inputReader.movementValue.x;
    }
    #endregion
}
