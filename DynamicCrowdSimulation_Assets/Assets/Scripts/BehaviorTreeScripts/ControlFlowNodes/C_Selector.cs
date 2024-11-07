using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Selector : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();
    private int currentIndex = 0;

    public C_Selector(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (currentIndex < nodes.Count)
        {
            nodes[currentIndex].OnEnter();
        }
    }

    public override NodeState Tick()
    {
        while (currentIndex < nodes.Count)
        {
            NodeState nodeState = nodes[currentIndex].Evaluate();

            if (nodeState == NodeState.SUCCESS)
            {
                state = NodeState.SUCCESS;
                nodes[currentIndex].OnExit();
                currentIndex = 0; // Reset for next run
                return state;
            }

            if (nodeState == NodeState.RUNNING)
            {
                state = NodeState.RUNNING;
                return state;
            }

            nodes[currentIndex].OnExit();
            currentIndex++;
            if (currentIndex < nodes.Count)
            {
                nodes[currentIndex].OnEnter();
            }
        }

        state = NodeState.FAILURE;
        currentIndex = 0; // Reset for next run
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
        if (currentIndex < nodes.Count)
        {
            nodes[currentIndex].OnExit();
        }
        currentIndex = 0;
    }
}