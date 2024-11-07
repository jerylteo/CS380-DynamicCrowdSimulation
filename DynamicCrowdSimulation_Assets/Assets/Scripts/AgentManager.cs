// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{

    public float minDelay = 0.5f; // Minimum delay in seconds
    public float maxDelay = 1.0f; // Maximum delay in seconds
    public int maxAgents = 200;

    public Material[] emotionMaterials;
    public GameObject TouristPrefab;
    public GameObject IceCreamManPrefab;

    public GameObject IceCreamMan;

    private int agentCount = 0;
    
    public enum Emotions {
        Idle,
        Happy,
        Angry,
        Disgusted
    };

    void Start()
    {
        SpawnIceCreamMan();
        StartCoroutine(RandomDelay());
    }

    IEnumerator RandomDelay()
    {
        while (true)
        {
            // Generate a random delay
            float randomDelay = Random.Range(minDelay, maxDelay);

            // Wait for the random delay
            yield return new WaitForSeconds(randomDelay);

            if (agentCount < maxAgents)
            {
                SpawnNewAgent();
            }
        }
    }

    void SpawnNewAgent()
    {
        ParkManager pm = GetComponent<ParkManager>();
        Transform entrance = pm.Entrances[Random.Range(0, pm.Entrances.Length)];
        Quaternion quat = Quaternion.Euler(0f, 90f, 45f);
        GameObject agent = Instantiate(TouristPrefab, entrance.position, quat);
        NavMeshAgent agentNavMesh = agent.GetComponent<NavMeshAgent>();
        agentNavMesh.updateRotation = false;
        ++agentCount;
    }

    public void DestroyAgent(GameObject agent)
    {
        Destroy(agent);
        --agentCount;
    }

    void SpawnIceCreamMan()
    {
        ParkManager pm = GetComponent<ParkManager>();
        Transform entrance = pm.Entrances[Random.Range(0, pm.Entrances.Length)];
        Quaternion quat = Quaternion.Euler(0f, 90f, 45f);
        IceCreamMan = Instantiate(IceCreamManPrefab, entrance.position, quat);
        NavMeshAgent agentNavMesh = IceCreamMan.GetComponent<NavMeshAgent>();
        agentNavMesh.avoidancePriority = 1;
        agentNavMesh.updateRotation = false;
    }

}