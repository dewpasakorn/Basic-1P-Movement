using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    public void SwitchState(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    void FixedUpdate()
    {
        currentState.Tick(Time.deltaTime);
    }
}
