using System.Collections.Generic;

public class GoapAStar
{
    protected int maxNodesToExpand;

    public GoapAStar(int maxNodesToExpand)
    {
        this.maxNodesToExpand = maxNodesToExpand;
    }

    public LinkedList<GoapAction> FindPath(GoapState initialState, GoapState goalState, List<GoapAction> availableActions, int maxIterations)
    {
        GoapNode goalNode = new GoapNode(null, goalState);

        List<GoapNode> open = new List<GoapNode>();
        List<GoapNode> closed = new List<GoapNode>();

        open.Add(goalNode);

        GoapNode current = null;

        int iterations = 0;

        while (open.Count > 0 && iterations < maxIterations && open.Count + 1 < maxNodesToExpand)
        {
            iterations++;

            open.Sort((x, y) => { return x.fScore < y.fScore ? -1 : 1; });

            current = open[0];

            if (current.state.IsSubstateOf(initialState))
            {
                break;
            }

            open.RemoveAt(0);
            closed.Add(current);

            foreach (GoapNode neighbour in current.GetAccessibleStates(availableActions))
            {
                bool stateIsClosed = false;
                foreach (GoapNode closedNode in closed)
                {
                    if (closedNode.state.IsCompleteMatchOf(neighbour.state))
                    {
                        stateIsClosed = true;
                    }
                }

                if (stateIsClosed) continue;

                float gScore = current.gScore + neighbour.action.cost;

                GoapNode similarNode = null;
                foreach (GoapNode openNode in open)
                {
                    if (openNode.state.IsCompleteMatchOf(neighbour.state))
                    {
                        similarNode = openNode;
                    }
                }

                if (similarNode != null)
                {
                    if (similarNode.gScore > gScore)
                        open.Remove(similarNode);
                    else
                        continue;
                }

                float heuristic = 0;
                neighbour.parent = current;
                neighbour.gScore = gScore;
                neighbour.fScore = gScore + heuristic;

                open.Add(neighbour);
            }            
        }

        LinkedList<GoapAction> path = new LinkedList<GoapAction>();

        if (!current.state.IsSubstateOf(initialState))
        {
            return path;
        }

        while (current != null)
        {
            if (current.action != null)
            {
                path.AddLast(current.action);
            }
            current = current.parent;
        }

        return path;
    }
}