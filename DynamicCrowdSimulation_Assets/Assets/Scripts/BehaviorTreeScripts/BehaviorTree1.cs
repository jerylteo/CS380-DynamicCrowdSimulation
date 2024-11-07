using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorTree1 : MonoBehaviour
{
    private BTNode root;

    //public Transform agent;
    public Transform target;
    public float range;

    //private bool ran = false;

    void Start()
    {

        // Ensure the agent has a NavMeshAgent component
        if (GetComponent<NavMeshAgent>() == null)
        {
            gameObject.AddComponent<NavMeshAgent>();
        }

        ParkManager pm = GameObject.Find("Park").GetComponent<ParkManager>();
        target = pm.Locations[Random.Range(0, pm.Locations.Length)];

        //ParkManager pm = GetComponent<ParkManager>();
        Transform entrance = pm.Entrances[Random.Range(0, pm.Entrances.Length)];

        ///////////////////////////////////////////////////////////////////////
        /// BEHAVIOR TREE 1
        ///////////////////////////////////////////////////////////////////////
        root = new C_ReactiveSequencer(new List<BTNode>
        {
            new C_ParallelSequencer(new List<BTNode>{
                new C_Sequencer(new List<BTNode>{
                    new D_RepeatXTimes(
                        new C_Sequencer(new List<BTNode>
                        {
                            new L_ChangeEmotions(transform, AgentManager.Emotions.Idle),
                            new L_MoveToLocation(transform),
                            new L_ChangeEmotions(transform),
                            new L_Wait(2,4)
                            //new D_Delay(new L_ChangeEmotions(transform), 1, 2)
                        }), 10, 15
                        ),
                    new L_ChangeEmotions(transform, AgentManager.Emotions.Idle),
                    new L_MoveToLocation(transform, entrance),
                    new D_Delay(new L_DestroyAgent(gameObject), 2, 3)
                }),
                new L_IsNearTarget(transform)
            }),
            new C_ParallelSequencer(new List<BTNode>
            {
                new C_Sequencer(new List<BTNode>{
                    new L_MoveToLocation(transform, L_MoveToLocation.targetLocations.ICECREAM_MAN),
                    new D_Delay(new L_ChangeEmotions(transform), 1, 2)
                }),
                new D_Inverter(new L_IsNearTarget(transform))
            }),
            new L_ChangeEmotions(transform, AgentManager.Emotions.Idle)
        });

    }

    void Update()
    {
        root.Evaluate();
    }
}
