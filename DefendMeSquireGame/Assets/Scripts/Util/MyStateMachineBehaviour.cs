using UnityEngine;

public abstract class MyStateMachineBehaviour : MonoBehaviour
{

    /// <summary>
    /// Called when the state is entered. Should be used to set up
    /// any internal state wiring of the state machine behaviour.
    /// Guaranteed to be called before the first OnStateUpdate
    /// </summary>
    public virtual void OnStateEnter() { }

    /// <summary>
    /// Called on every Update.
    /// </summary>
    /// <returns>True when it is time to transition to the next state. False otherwise--></returns>
    public abstract bool OnStateUpdate();

    /// <summary>
    /// Called when transitioning to the next state. Should be used
    /// to do any necessary cleanup of resources. Called after OnStateUpdate.
    /// </summary>
    public virtual void OnStateExit() { }
}
