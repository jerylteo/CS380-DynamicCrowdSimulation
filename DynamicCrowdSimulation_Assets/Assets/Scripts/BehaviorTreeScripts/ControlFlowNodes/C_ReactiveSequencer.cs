using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ReactiveSequencer : BTNode
{
    private List<BTNode> nodes;
    private int currentIndex;

    public C_ReactiveSequencer(List<BTNode> nodes)
    {
        this.nodes = nodes;
        this.currentIndex = 0;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        currentIndex = 0;
    }

    public override NodeState Tick()
    {
        if (currentIndex < nodes.Count)
        {
            NodeState nodeState = nodes[currentIndex].Evaluate();

            if (nodeState == NodeState.RUNNING)
            {
                state = NodeState.RUNNING;
                return state;
            }

            if (nodeState == NodeState.FAILURE)
            {
                currentIndex++; // Move to the next child
                state = NodeState.RUNNING;
                return state;
            }

            if (nodeState == NodeState.SUCCESS)
            {
                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
        currentIndex = 0;
    }
}