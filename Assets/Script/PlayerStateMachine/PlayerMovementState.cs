using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.inputReader.OnJumpEvent += Jump;
    }

    public override void Tick(float deltaTime)
    {
        MovementHandle(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.inputReader.OnJumpEvent -= Jump;
    }
}
