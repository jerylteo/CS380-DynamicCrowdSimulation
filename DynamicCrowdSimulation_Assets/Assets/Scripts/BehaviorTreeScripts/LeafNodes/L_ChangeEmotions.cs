using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L_ChangeEmotions : BTNode
{
    private MeshRenderer targetRenderer;
    private AgentManager agentManager;
    private ParkManager parkManager;
    private Transform agent;
    private bool isEmotionGiven = false;
    private AgentManager.Emotions newEmotion;

    public L_ChangeEmotions(Transform agent)
    {
        this.agent = agent;
        GameObject park = GameObject.Find("Park");
        agentManager = park.GetComponent<AgentManager>();
        parkManager = park.GetComponent<ParkManager>();
        targetRenderer = agent.GetComponent<MeshRenderer>();
    }

    public L_ChangeEmotions(Transform agent, AgentManager.Emotions emotion)
    {
        this.agent = agent;
        GameObject park = GameObject.Find("Park");
        agentManager = park.GetComponent<AgentManager>();
        parkManager = park.GetComponent<ParkManager>();
        targetRenderer = agent.GetComponent<MeshRenderer>();
        isEmotionGiven = true;
        newEmotion = emotion;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // Cafe, Garden, Fountain, Toilet, Playground
        // Idle, Happy, Angry, Disgusted
        Material chosenMat;
        if (isEmotionGiven) {           // When given an emotion. E.g. Before L_MoveToLocation
            chosenMat = agentManager.emotionMaterials[(int)newEmotion];
        }
        else {                          // When not given an emotion. E.g. After L_MoveToLocation (Automatic)
            if (Vector3.Distance(agent.position, parkManager.Locations[3].position) < 3f) {
                chosenMat = agentManager.emotionMaterials[3];
            }
            else {
                chosenMat = agentManager.emotionMaterials[1];
            }
        }

        // Perform actions to change emoji to Happy, e.g., changing the sprite or triggering an animation
        // For now: Default to Happy:
        // targetRenderer.material = agentManager.emotionMaterials[Random.Range(0, agentManager.emotionMaterials.Count())];
        targetRenderer.material = chosenMat;
    }

    public override NodeState Tick()
    {
        // Since the action is immediate, we don't have to do anything in Tick
        //Debug.Log("ChangeEmotions - tick");
        state = NodeState.SUCCESS;
        return state;
    }

}