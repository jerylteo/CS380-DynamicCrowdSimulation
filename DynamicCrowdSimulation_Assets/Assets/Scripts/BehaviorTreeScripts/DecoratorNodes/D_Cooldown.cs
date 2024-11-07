using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cooldown before child node can be executed again
public class D_Cooldown : BTNode
{
    private BTNode child;
    private float cooldownTime;
    private float lastExecutionTime;

    public D_Cooldown(BTNode child, float cooldownTime)
    {
        this.child = child;
        this.cooldownTime = cooldownTime;
        this.lastExecutionTime = -cooldownTime; // Ensure it can run immediately the first time
    }

    public D_Cooldown(BTNode child, float minCooldown, float maxCooldown) {
        this.child = child;
        this.cooldownTime = Random.Range(minCooldown, maxCooldown);
        this.lastExecutionTime = -cooldownTime;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        // No specific initialization required for cooldown
    }

    public override NodeState Tick()
    {
        if (Time.time - lastExecutionTime >= cooldownTime)
        {
            NodeState childState = child.Evaluate();
            if (childState == NodeState.RUNNING)
            {
                return NodeState.RUNNING;
            }

            // Set the last execution time after child node execution
            lastExecutionTime = Time.time;

            // Flip the result for cooldown behavior
            state = (childState == NodeState.SUCCESS) ? NodeState.FAILURE : NodeState.SUCCESS;
            return state;
        }

        // Still in cooldown period
        return NodeState.FAILURE;
    }

}