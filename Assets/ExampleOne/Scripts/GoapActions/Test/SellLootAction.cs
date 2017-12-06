using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellLootAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.HAS_LOOT, true);
        effects.stateProperties.Add(StatePropertyKey.HAS_LOOT, false);
    }

    public override void Execute(AIPlayer player)
    {
        
    }

    protected override bool GetContextualValidation(GoapState desiredState)
    {
        if (!desiredState.stateProperties.ContainsKey(StatePropertyKey.TOTAL_MONEY))
        {
            return false;
        }

        if ((int)desiredState.stateProperties[StatePropertyKey.TOTAL_MONEY] <= 0)
        {
            return false;
        }

        return true;
    }

    protected override GoapState GetVariablePreconditions(GoapState desiredState)
    {
        GoapState variablePreconditions = new GoapState();

        variablePreconditions.stateProperties.Add(StatePropertyKey.TOTAL_MONEY, (int)desiredState.stateProperties[StatePropertyKey.TOTAL_MONEY] - 1);

        return variablePreconditions;
    }

    protected override GoapState GetVariableEffects(GoapState desiredState)
    {
        GoapState variableEffects = new GoapState();

        variableEffects.stateProperties.Add(StatePropertyKey.TOTAL_MONEY, (int)desiredState.stateProperties[StatePropertyKey.TOTAL_MONEY]);

        return variableEffects;
    }
}