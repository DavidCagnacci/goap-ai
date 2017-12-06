using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum StatePropertyKey
{
    HAS_LOOT,
    TOTAL_MONEY,
    IDLE_WORKER_AVAILABLE,
    IDLE_WORKER_SELECTED,
    WORKERS_GATHERING,
    BASE_SELECTED,
    FIGHTERS_CREATED
}

public class GoapPlanner
{
    public LinkedList<GoapAction> GetPlan(GoapState initialState, GoapState goalState, List<GoapAction> availableActions, int maxIterations = 100)
    {
        GoapAStar aStar = new GoapAStar(1000);

        LinkedList<GoapAction> plan = aStar.FindPath(initialState, goalState, availableActions, maxIterations);

        if (plan.Count == 0)
        {
            Debug.Log("No plan found.");
        }

        return plan;
    }
}