using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMonsterAction : GoapAction
{
    void Awake()
    {
        preconditions.stateProperties.Add(StatePropertyKey.HAS_LOOT, false);
        effects.stateProperties.Add(StatePropertyKey.HAS_LOOT, true);
    }

    public override void Execute(AIPlayer player)
    {
        
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