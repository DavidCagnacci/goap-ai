using System.Collections.Generic;

public class GoapNode
{
    public GoapAction action;
    public GoapState state;
    public GoapNode parent;
    public float fScore;
    public float gScore;
    

    public GoapNode(GoapAction action, GoapState state)
    {
        this.action = action;
        this.state = new GoapState();

        foreach (KeyValuePair<StatePropertyKey, object> stateProperty in state.stateProperties)
        {
            this.state.stateProperties.Add(stateProperty.Key, stateProperty.Value);
        }
    }

    public List<GoapNode> GetAccessibleStates(List<GoapAction> availableActions)
    {
        List<GoapNode> returnList = new List<GoapNode>();

        foreach (GoapAction action in availableActions)
        {
            if (action.IsValidAction(state))
            {
                GoapState currentState = action.GetStateForNode(state);

                GoapNode node = new GoapNode(action, currentState);

                returnList.Add(node);
            }
        }

        return returnList;
    }
}