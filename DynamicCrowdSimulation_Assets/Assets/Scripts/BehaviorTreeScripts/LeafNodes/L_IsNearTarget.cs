using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class L_IsNearTarget : BTNode
{
    private Transform agentTransform;
    private AgentManager agentManager;
    private Transform targetTransform;

    private float distanceFromTarget = 5.0f;

    public L_IsNearTarget(Transform agent)
    {
        agentTransform = agent;
        agentManager = GameObject.Find("Park").GetComponent<AgentManager>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        targetTransform = agentManager.IceCreamMan.transform;
    }

    public override NodeState Tick()
    {
        float distance = Vector3.Distance(agentTransform.position, targetTransform.position);
        if (distance < distanceFromTarget)
        {
            state = NodeState.FAILURE;
        }
        else
        {
            state = NodeState.SUCCESS;
        }
        return state;
    }
}
