using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Wait : BTNode
{
    private float minDelay;
    private float maxDelay;
    private float delay;
    private float elapsedTime;

    public L_Wait(float minDelay, float maxDelay)
    {
        this.minDelay = minDelay;
        this.maxDelay = maxDelay;
        this.elapsedTime = 0.0f;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        delay = Random.Range(minDelay, maxDelay); // Flexible delay range
        elapsedTime = 0.0f;
    }

    public override NodeState Tick()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= delay)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}