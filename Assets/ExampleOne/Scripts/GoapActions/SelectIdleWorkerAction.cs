using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIdleWorkerAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.IDLE_WORKER_SELECTED, false);
        preconditions.stateProperties.Add(StatePropertyKey.IDLE_WORKER_AVAILABLE, true);
        effects.stateProperties.Add(StatePropertyKey.BASE_SELECTED, false);
        effects.stateProperties.Add(StatePropertyKey.IDLE_WORKER_SELECTED, true);
        effects.stateProperties.Add(StatePropertyKey.IDLE_WORKER_AVAILABLE, false);
    }

    public override void Execute(AIPlayer player)
    {
        GameObject worker = FindAvailableWorker();
        if (worker != null)
        {
            player.SelectActor(worker);
            isComplete = true;
        }
    }

    protected override bool GetContextualValidation(GoapState desiredState)
    {
        return true;
    }

    protected override GoapState GetVariablePreconditions(GoapState desiredState)
    {
        return null;
    }

    protected override GoapState GetVariableEffects(GoapState desiredState)
    {
        return null;
    }

    protected GameObject FindAvailableWorker()
    {
        GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");

        foreach (GameObject worker in workers)
        {
            if (worker.GetComponent<Unit>().GetCommand() == null)
            {
                return worker;
            }
        }

        return null;
    }
}