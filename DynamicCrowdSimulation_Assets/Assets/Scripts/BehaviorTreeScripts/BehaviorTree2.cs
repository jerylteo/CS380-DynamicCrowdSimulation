using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorTree2 : MonoBehaviour
{
    private BTNode root;

    //public Transform agent;
    public Transform target;
    public float range;

    //private bool ran = false;

    void Start()
    {
        ///////////////////////////////////////////////////////////////////////
        /// BEHAVIOR TREE 2
        ///////////////////////////////////////////////////////////////////////

        root = new C_Sequencer(new List<BTNode>
        {
            new C_Sequencer(new List<BTNode>
            {
                new L_MoveToLocation(transform, L_MoveToLocation.targetLocations.RANDOM_LOCATION),
                new L_Wait(12,16)
            })
        });
    }

    void Update()
    {
        root.Evaluate();
    }
}
