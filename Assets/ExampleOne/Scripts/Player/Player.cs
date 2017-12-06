using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject selectionIndicator;
    public uint startingResources;

    protected GameObject selectedActor;
    protected uint resourceCount = 0;

    protected virtual void Start()
    {
        resourceCount = startingResources;
    }

    protected virtual void Update()
    {
        if (selectedActor != null)
        {
            selectionIndicator.transform.position = selectedActor.transform.position;
        }
        else
        {
            selectionIndicator.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public uint GetResourceCount()
    {
        return resourceCount;
    }

    public void AddResources(uint amount)
    {
        resourceCount += amount;
    }

    public void SubtractResources(uint amount)
    {
        resourceCount -= amount;
    }

    public void SelectActor(GameObject actor)
    {
        if (actor == null) return;

        if (selectedActor != null)
        {
            selectedActor.GetComponent<Actor>().OnDeselect();
        }
        selectedActor = actor;
        selectedActor.GetComponent<Actor>().OnSelect();
        selectionIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    public GameObject GetSelectedActor()
    {
        return selectedActor;
    }

    public void IssueMoveCommand(GameObject unit, Vector2 position)
    {
        selectedActor.GetComponent<Unit>().ReceiveCommand(new MoveCommand(selectedActor, position));
    }

    public void IssueGatherCommand(GameObject unit, GameObject resourceNode)
    {
        selectedActor.GetComponent<Unit>().ReceiveCommand(new GatherCommand(selectedActor, resourceNode));
    }
}
