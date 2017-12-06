using UnityEngine;

public abstract class UnitCommand
{
    protected Unit unit;

    public UnitCommand(GameObject unit)
    {
        this.unit = unit.GetComponent<Unit>();
    }

    public abstract void Execute();
}

public class MoveCommand : UnitCommand
{
    protected Vector3 position;

    public MoveCommand(GameObject unit, Vector3 position) : base(unit)
    {
        this.position = position;
    }

    public override void Execute()
    {
        unit.MoveTowards(position);

        if (unit.transform.position == position)
        {
            unit.ReceiveCommand(null);
        }
    }
}

public class GatherCommand : UnitCommand
{
    protected GameObject resourceNode;
    protected GameObject dropOffPoint;
    protected bool unitIsCarryingResources = false;

    public GatherCommand(GameObject unit, GameObject resourceNode) : base(unit)
    {
        this.resourceNode = resourceNode;
    }

    public override void Execute()
    {

        if (unitIsCarryingResources)
        {
            if (dropOffPoint == null)
            {
                dropOffPoint = FindNearestDropOffPoint();
                if (dropOffPoint == null)
                {
                    unit.ReceiveCommand(null);
                }
            }
            else
            {
                if (unit.transform.position == dropOffPoint.transform.position)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddResources(1);
                    unitIsCarryingResources = false;
                    dropOffPoint = null;
                }
                else
                {
                    unit.MoveTowards(dropOffPoint.transform.position);
                }
            }
        }
        else
        {

            if (unit.transform.position == resourceNode.transform.position)
            {
                unitIsCarryingResources = true;
            }
            else
            {
                unit.MoveTowards(resourceNode.transform.position);
            }
        }
    }

    public GameObject FindNearestDropOffPoint()
    {
        GameObject[] dropOffPoints = GameObject.FindGameObjectsWithTag("Structure");

        float distance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject dropOffPoint in dropOffPoints)
        {
            float curDistance = Vector3.Distance(unit.transform.position, dropOffPoint.transform.position);
            if (curDistance < distance)
            {
                closest = dropOffPoint;
                distance = curDistance;
            }
        }

        return closest;
    }
}