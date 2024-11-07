using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BTNode
{
    public enum NodeState
    {
        INACTIVE,
        RUNNING,
        SUCCESS,
        FAILURE
    }

    protected NodeState state = NodeState.INACTIVE;

    public NodeState State { get { return state; } }

    public NodeState Evaluate()
    {
        if (state == NodeState.INACTIVE)
        {
            OnEnter();
            state = NodeState.RUNNING;
        }

        state = Tick();

        if (state != NodeState.RUNNING)
        {
            OnExit();
            NodeState tempState = state;
            state = NodeState.INACTIVE;
            return tempState;
        }

        return state;
    }

    public abstract NodeState Tick();

    public virtual void OnEnter() { }

    public virtual void OnExit() { }
}