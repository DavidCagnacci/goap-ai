using System.Collections.Generic;

public class GoapState
{
    public Dictionary<StatePropertyKey, object> stateProperties = new Dictionary<StatePropertyKey, object>();

    public bool IsSubstateOf(GoapState superState)
    {
        bool allMatch = true;

        foreach (KeyValuePair<StatePropertyKey, object> substateProperty in stateProperties)
        {
            bool match = false;
            foreach (KeyValuePair<StatePropertyKey, object> stateProperty in superState.stateProperties)
            {
                if (stateProperty.Equals(substateProperty))
                {
                    match = true;
                }
            }

            if (!match)
            {
                if (substateProperty.Value as int? != 0 && substateProperty.Value as bool? != false)
                {
                    allMatch = false;
                }
            }
        }

        return allMatch;
    }

    public bool IsCompleteMatchOf(GoapState otherState)
    {
        return IsSubstateOf(otherState) && otherState.IsSubstateOf(this);
    }
}
