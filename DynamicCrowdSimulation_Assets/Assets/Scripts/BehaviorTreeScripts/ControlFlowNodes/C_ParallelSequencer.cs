using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ParallelSequencer : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public C_ParallelSequencer(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //foreach (BTNode node in nodes)
        //{
        //    node.OnEnter();
        //}
    }

    public override NodeState Tick()
    {
        bool allSucceeded = true;

        foreach (BTNode node in nodes)
        {
            NodeState nodeState = node.Evaluate();

            if (nodeState == NodeState.FAILURE)
            {
                state = NodeState.FAILURE;
                foreach (BTNode n in nodes)
                {
                    n.OnExit();
                }
                return state;
            }

            if (nodeState == NodeState.RUNNING)
            {
                allSucceeded = false;
            }
        }

        state = allSucceeded ? NodeState.SUCCESS : NodeState.RUNNING;
        if (state != NodeState.RUNNING)
        {
            foreach (BTNode node in nodes)
            {
                node.OnExit();
            }
        }
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
        //foreach (BTNode node in nodes)
        //{
        //    node.OnExit();
        //}
    }
}