using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorkerAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.IDLE_WORKER_AVAILABLE, false);
        preconditions.stateProperties.Add(StatePropertyKey.BASE_SELECTED, true);
        effects.stateProperties.Add(StatePropertyKey.IDLE_WORKER_AVAILABLE, true);
    }

    public override void Execute(AIPlayer player)
    {
        if (player.GetSelectedActor().GetComponent<Structure>().CreateWorker())
        {
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
}