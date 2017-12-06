using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFighterAction : GoapAction
{
    public int workersRequired;

    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.WORKERS_GATHERING, workersRequired);
        preconditions.stateProperties.Add(StatePropertyKey.BASE_SELECTED, true);
    }

    public override void Execute(AIPlayer player)
    {
        if (player.GetSelectedActor().GetComponent<Structure>().CreateFighter())
        {
            isComplete = true;
        }
    }

    protected override bool GetContextualValidation(GoapState desiredState)
    {
        if (!desiredState.stateProperties.ContainsKey(StatePropertyKey.FIGHTERS_CREATED))
        {
            return false;
        }

        if ((int)desiredState.stateProperties[StatePropertyKey.FIGHTERS_CREATED] <= 0)
        {
            return false;
        }

        return true;
    }

    protected override GoapState GetVariablePreconditions(GoapState desiredState)
    {
        GoapState variablePreconditions = new GoapState();

        variablePreconditions.stateProperties.Add(StatePropertyKey.FIGHTERS_CREATED, (int)desiredState.stateProperties[StatePropertyKey.FIGHTERS_CREATED] - 1);

        return variablePreconditions;
    }

    protected override GoapState GetVariableEffects(GoapState desiredState)
    {
        GoapState variableEffects = new GoapState();

        variableEffects.stateProperties.Add(StatePropertyKey.FIGHTERS_CREATED, (int)desiredState.stateProperties[StatePropertyKey.FIGHTERS_CREATED]);

        return variableEffects;
    }
}