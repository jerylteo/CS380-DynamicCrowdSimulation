using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Inverter : BTNode
{
    private BTNode child;

    public D_Inverter(BTNode child)
    {
        this.child = child;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override NodeState Tick()
    {
        NodeState childState = child.Evaluate();

        switch (childState)
        {
            case NodeState.SUCCESS:
                state = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                state = NodeState.SUCCESS;
                break;
            case NodeState.RUNNING:
                state = NodeState.RUNNING;
                break;
        }
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
