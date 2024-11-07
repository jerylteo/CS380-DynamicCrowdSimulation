using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_RandomSelector : BTNode
{
    private List<BTNode> nodes;
    private List<bool> nodeStatus;
    private int randomIndex = -1;

    public C_RandomSelector(List<BTNode> nodes)
    {
        this.nodes = nodes;
        nodeStatus = new List<bool>(new bool[nodes.Count]);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        chooseRandomNode();
    }

    public override NodeState Tick()
    {
        if (randomIndex < 0 || randomIndex >= nodes.Count)
        {
            state = NodeState.FAILURE;
            return state;
        }

        NodeState nodeState = nodes[randomIndex].Evaluate();

        if (nodeState == NodeState.SUCCESS)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        if (nodeState == NodeState.FAILURE)
        {
            nodeStatus[randomIndex] = true;

            if (checkForAllFailed())
            {
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                chooseRandomNode();
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
        foreach (BTNode node in nodes)
        {
            node.OnExit();
        }
    }

    private void chooseRandomNode()
    {
        List<int> remainingIndices = new List<int>();
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!nodeStatus[i])
            {
                remainingIndices.Add(i);
            }
        }

        if (remainingIndices.Count > 0)
        {
            int randomChoice = Random.Range(0, remainingIndices.Count);
            randomIndex = remainingIndices[randomChoice];
            nodes[randomIndex].OnEnter();
        }
        else
        {
            randomIndex = -1; // All nodes have failed
        }
    }

    private bool checkForAllFailed()
    {
        foreach (bool status in nodeStatus)
        {
            if (!status)
            {
                return false;
            }
        }
        return true;
    }
}