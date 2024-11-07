using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_RepeatXTimes : BTNode
{
    private BTNode child;
    private int remainingRepeats;
    private int initialRepeats;
    private int minRepeat;
    private int maxRepeat;
    private bool countProvided = false;

    public D_RepeatXTimes(BTNode child, int repeatCount)
    {
        this.child = child;
        this.initialRepeats = repeatCount;
        this.remainingRepeats = repeatCount;
        countProvided = true;
    }

    public D_RepeatXTimes(BTNode child, int minRepeat, int maxRepeat) {
        this.child = child;
        this.minRepeat = minRepeat;
        this.maxRepeat = maxRepeat;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (!countProvided)
        {
            int randomAmount = Random.Range(minRepeat, maxRepeat);
            initialRepeats = randomAmount;
        }
        remainingRepeats = initialRepeats; // Reset remaining repeats when entering
    }

    public override NodeState Tick()
    {
        NodeState childState = child.Evaluate();

        if (childState == NodeState.SUCCESS)
        {
            remainingRepeats--;
            if (remainingRepeats == 0)
            {
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.RUNNING;
            }
        }
        else if (childState == NodeState.FAILURE)
        {
            state = NodeState.FAILURE;
        }
        else
        {
            state = NodeState.RUNNING;
        }

        return state;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}