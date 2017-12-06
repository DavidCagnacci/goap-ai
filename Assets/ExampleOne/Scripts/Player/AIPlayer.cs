using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer : Player
{
    public float actionRate;
    public int totalFightersGoal;

    protected GoapPlanner planner = new GoapPlanner();
    protected LinkedList<GoapAction> currentActions;
    protected float actionTimer = 0;
    protected List<GoapAction> availableActions = new List<GoapAction>();
    protected GoapState state = new GoapState();
    protected GoapState goal = new GoapState();
    protected bool isPaused = true;

    protected override void Start()
    {
        base.Start();

        GoapAction[] actions = gameObject.GetComponents<GoapAction>();
        foreach (GoapAction a in actions)
        {
            availableActions.Add(a);
        }
        
        goal.stateProperties.Add(StatePropertyKey.FIGHTERS_CREATED, totalFightersGoal);

        LinkedList<GoapAction> plan = planner.GetPlan(state, goal, availableActions, 1000);

        int counter = 1;
        foreach (GoapAction a in plan)
        {
            Debug.Log(counter++ + ". " + a);
        }

        if (plan != null)
        {
            currentActions = plan;
        }
    }

    protected override void Update()
    {
        if (Input.GetAxis("Submit") != 0)
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            return;
        }

        base.Update();

        actionTimer += Time.deltaTime;

        if (actionTimer >= actionRate)
        {
            if (currentActions != null && currentActions.Count > 0)
            {
                GoapAction action = currentActions.First.Value;

                if (action != null)
                {
                    action.Execute(this);

                    if (action.IsComplete)
                    {
                        currentActions.RemoveFirst();
                        if (currentActions.Count > 0)
                        {
                            currentActions.First.Value.IsComplete = false;
                        }
                    }
                }

                actionTimer = 0;
            }
        }
	}

    public GoapAction GetCurrentAction()
    {
        if (currentActions.Count > 0)
        {
            return currentActions.First.Value;
        }
        else
        {
            return null;
        }
    }

    public GoapAction GetNextAction()
    {
        if (currentActions.Count > 1)
        {
            return currentActions.First.Next.Value;
        }
        else
        {
            return null;
        }
    }
}