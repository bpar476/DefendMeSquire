using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a state machine. Currently only supports linear, acyclic state machines i.e.
/// non-branching and has a final, stable state.
/// </summary>
public class MyStateMachine : MonoBehaviour
{
    [SerializeField]
    private MyStateMachineBehaviour[] sequence;

    private MyStateMachineBehaviour currentState;
    private int currentStateIndex = 0;

    private void Awake()
    {
        if (sequence.Length == 0)
        {
            this.enabled = false;
        }
        else
        {
            foreach (var state in sequence)
            {
                state.enabled = false;
            }

            currentState = sequence[currentStateIndex];
            currentState.enabled = true;
            currentState.OnStateEnter();
        }
    }

    private void Update()
    {
        if (currentState.OnStateUpdate())
        {
            currentStateIndex++;

            currentState.OnStateExit();

            if (currentStateIndex >= sequence.Length)
            {
                this.enabled = false;
                return;
            }

            currentState = sequence[currentStateIndex];
            currentState.OnStateEnter();
        }
    }
}
