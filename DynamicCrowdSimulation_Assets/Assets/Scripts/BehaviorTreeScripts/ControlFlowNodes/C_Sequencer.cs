using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Sequencer : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();
    private int currentIndex = 0;

    public C_Sequencer(List<BTNode> nodes)
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

            if (nodeState == NodeState.FAILURE)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (nodeState == NodeState.SUCCESS)
            {
                currentIndex++;
            }
            
            state = NodeState.RUNNING;
            return state;
        }

        state = NodeState.SUCCESS;
        currentIndex = 0; // Reset for next run
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
        currentIndex = 0;
    }
}