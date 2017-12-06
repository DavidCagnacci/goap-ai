using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBaseAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.BASE_SELECTED, false);
        effects.stateProperties.Add(StatePropertyKey.BASE_SELECTED, true);
        effects.stateProperties.Add(StatePropertyKey.IDLE_WORKER_SELECTED, false);
    }

    public override void Execute(AIPlayer player)
    {
        player.SelectActor(GameObject.FindGameObjectWithTag("Structure"));
        isComplete = true;
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