using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    protected bool isComplete = false;
    protected GoapState preconditions = new GoapState();
    protected GoapState effects = new GoapState();

    public float cost;

    public abstract void Execute(AIPlayer player);


    protected abstract bool GetContextualValidation(GoapState desiredState);
    protected abstract GoapState GetVariablePreconditions(GoapState desiredState);
    protected abstract GoapState GetVariableEffects(GoapState desiredState);

    public bool IsValidAction(GoapState fromState)
    {
        bool isValid = GetContextualValidation(fromState);

        if (isValid)
        {
            GoapState allEffects = GetEffects(fromState);

            foreach (KeyValuePair<StatePropertyKey, object> effect in allEffects.stateProperties)
            {
                foreach (KeyValuePair<StatePropertyKey, object> sp in fromState.stateProperties)
                {
                    if (effect.Equals(sp))
                    {
                        isValid = true;
                    }
                }
            }

            foreach (KeyValuePair<StatePropertyKey, object> effect in allEffects.stateProperties)
            {
                foreach (KeyValuePair<StatePropertyKey, object> sp in fromState.stateProperties)
                {
                    if (effect.Key == sp.Key && !effect.Equals(sp))
                    {
                        isValid = false;
                    }
                }
            }
        }

        return isValid;
    }

    public GoapState GetStateForNode(GoapState fromState)
    {
        GoapState newState = new GoapState();

        foreach (KeyValuePair<StatePropertyKey, object> stateProperty in fromState.stateProperties)
        {
            newState.stateProperties[stateProperty.Key] = stateProperty.Value;
        }

        GoapState allPreconditions = GetPreconditions(fromState);

        foreach (KeyValuePair<StatePropertyKey, object> precondition in allPreconditions.stateProperties)
        {
            newState.stateProperties[precondition.Key] = precondition.Value;
        }

        return newState;
    }

    public bool IsComplete
    {
        get
        {
            return isComplete;
        }

        set
        {
            isComplete = value;
        }
    }

    public GoapState GetPreconditions(GoapState desiredState)
    {
        GoapState allPreconditions = GetVariablePreconditions(desiredState);

        if (allPreconditions == null)
        {
            allPreconditions = new GoapState();
        }

        foreach (KeyValuePair<StatePropertyKey, object> stateProperty in preconditions.stateProperties)
        {
            allPreconditions.stateProperties.Add(stateProperty.Key, stateProperty.Value);
        }

        return allPreconditions;
    }

    public GoapState GetEffects(GoapState desiredState)
    {
        GoapState allEffects = GetVariableEffects(desiredState);

        if (allEffects == null)
        {
            allEffects = new GoapState();
        }

        foreach (KeyValuePair<StatePropertyKey, object> stateProperty in effects.stateProperties)
        {
            allEffects.stateProperties.Add(stateProperty.Key, stateProperty.Value);
        }

        return allEffects;
    }
}