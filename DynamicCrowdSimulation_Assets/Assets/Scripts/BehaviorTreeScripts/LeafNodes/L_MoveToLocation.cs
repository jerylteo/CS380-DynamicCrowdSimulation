using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BTNode;
using UnityEngine.AI;
using System;
using System.Collections.ObjectModel;

public class L_MoveToLocation : BTNode
{
    private Transform targetTransform;
    private NavMeshAgent navMeshAgent;
    private ParkManager parkManager;
    private AgentManager agentManager;
    private bool targetAcquired = false;
    private bool iceCreamMan = false;
    private targetLocations location;
    private float elapsedTime = 0.0f;
    private Transform agent;

    public enum targetLocations
    {
        NONE, // 0
        ICECREAM_MAN, // 1
        RANDOM_LOCATION // 2
    }

    public L_MoveToLocation(Transform agent, Transform target)
    {
        this.agent = agent;
        targetTransform = target;
        targetAcquired = true;
        location = targetLocations.NONE;
        iceCreamMan = false;
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        parkManager = GameObject.Find("Park").GetComponent<ParkManager>();
        agentManager = GameObject.Find("Park").GetComponent<AgentManager>();
    }

    public L_MoveToLocation(Transform agent, targetLocations loc)
    {
        this.agent = agent;
        location = loc;
        iceCreamMan = true;
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        parkManager = GameObject.Find("Park").GetComponent<ParkManager>();
        agentManager = GameObject.Find("Park").GetComponent<AgentManager>();
    }

    public L_MoveToLocation(Transform agent)
    {
        this.agent = agent;
        location = targetLocations.NONE;
        iceCreamMan = false;
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        parkManager = GameObject.Find("Park").GetComponent<ParkManager>();
        agentManager = GameObject.Find("Park").GetComponent<AgentManager>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //location = targetLocations.NONE;
        if (navMeshAgent != null)
        {
            if (!targetAcquired)
            {
                if (iceCreamMan)
                {
                    switch(location)
                    {
                        case targetLocations.ICECREAM_MAN:
                            targetTransform = agentManager.IceCreamMan.transform;
                            break;
                        case targetLocations.RANDOM_LOCATION:
                            // random location
                            bool found = false;
                            float x = 0;
                            float z = 0;
                            while (!found)
                            {
                                Vector3 min = parkManager.GetComponent<MeshRenderer>().bounds.min;
                                Vector3 max = parkManager.GetComponent<MeshRenderer>().bounds.max;
                                x = UnityEngine.Random.Range(min.x, max.x);
                                z = UnityEngine.Random.Range(min.z, max.z);
                                found = true;
                                foreach (var coordinate in parkManager.Locations)
                                {
                                    Debug.Log("Coordinate = " + coordinate.position.x + ", " + coordinate.position.y + ", " + coordinate.position.z);
                                    Debug.Log("New Pos = " + x + ", 0, " + z);
                                    Debug.Log("Distance = " + Vector3.Distance(coordinate.position, new Vector3(x, 0, z)));
                                    if (Vector3.Distance(coordinate.position, new Vector3(x, 0, z)) < 4)
                                    {
                                        found = false;
                                        break;
                                    }
                                }
                            }
                            targetTransform = new GameObject().transform;
                            targetTransform.position = new Vector3(x, 0, z);
                            break;
                    }
                }
                else
                {
                    location = targetLocations.NONE;
                    targetTransform = parkManager.Locations[UnityEngine.Random.Range(0, parkManager.Locations.Length)];
                }
            }
            else
            {
                location = targetLocations.NONE;
            }
            navMeshAgent.SetDestination(targetTransform.position);
        }
        elapsedTime = 0.0f;
    }

    public override NodeState Tick()
    {

        if (!iceCreamMan)
        {
            location = targetLocations.NONE;
        }

        if (navMeshAgent == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (navMeshAgent.pathPending)
        {
            state = NodeState.RUNNING;
            return state;
        }

        if (location == targetLocations.ICECREAM_MAN && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 2f)
        {
            elapsedTime = 0.0f;
            //location = targetLocations.NONE;
            state = NodeState.SUCCESS;
            return state;
        }

        else if (location != targetLocations.ICECREAM_MAN && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 3f)
        {
            elapsedTime = 0.0f;
            //location = (location ==  targetLocations.ICECREAM_MAN) ? targetLocations.NONE : location;
            state = NodeState.SUCCESS;
            return state;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 3.0f && location == targetLocations.ICECREAM_MAN)
        {
            agent.GetComponent<MeshRenderer>().material = agentManager.emotionMaterials[2];
        }

        state = NodeState.RUNNING;
        return state;
    }
}
