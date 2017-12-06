using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendWorkerToGatherAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.IDLE_WORKER_SELECTED, true);
        effects.stateProperties.Add(StatePropertyKey.IDLE_WORKER_SELECTED, false);
    }

    public override void Execute(AIPlayer player)
    {
        GameObject resourceNode = FindNearestResourceNode(player.GetSelectedActor());
        if (resourceNode != null)
        {
            player.IssueGatherCommand(player.GetSelectedActor(), resourceNode);
            isComplete = true;
        }
    }

    protected override bool GetContextualValidation(GoapState desiredState)
    {
        if (!desiredState.stateProperties.ContainsKey(StatePropertyKey.WORKERS_GATHERING))
        {
            return false;
        }

        if ((int)desiredState.stateProperties[StatePropertyKey.WORKERS_GATHERING] <= 0)
        {
            return false;
        }

        return true;
    }

    protected override GoapState GetVariablePreconditions(GoapState desiredState)
    {
        GoapState variablePreconditions = new GoapState();

        variablePreconditions.stateProperties.Add(StatePropertyKey.WORKERS_GATHERING, (int)desiredState.stateProperties[StatePropertyKey.WORKERS_GATHERING] - 1);

        return variablePreconditions;
    }

    protected override GoapState GetVariableEffects(GoapState desiredState)
    {
        GoapState variableEffects = new GoapState();
        
        variableEffects.stateProperties.Add(StatePropertyKey.WORKERS_GATHERING, (int)desiredState.stateProperties[StatePropertyKey.WORKERS_GATHERING]);

        return variableEffects;
    }

    protected GameObject FindNearestResourceNode(GameObject worker)
    {
        GameObject[] resourceNodes = GameObject.FindGameObjectsWithTag("ResourceNode");

        float distance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject resourceNode in resourceNodes)
        {
            float curDistance = Vector3.Distance(worker.transform.position, resourceNode.transform.position);
            if (curDistance < distance)
            {
                closest = resourceNode;
                distance = curDistance;
            }
        }

        return closest;
    }
}