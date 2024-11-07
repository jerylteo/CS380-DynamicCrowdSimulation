using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_DestroyAgent : BTNode
{
    [HideInInspector]
    public GameObject agentHandle;

    public L_DestroyAgent(GameObject agent)
    {
        agentHandle = agent;
    }

    public override void OnEnter()
    {
        AgentManager am = GameObject.Find("Park").GetComponent<AgentManager>();
        am.DestroyAgent(agentHandle);
    }

    public override NodeState Tick()
    {
        return NodeState.SUCCESS;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
