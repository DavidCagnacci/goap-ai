using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Actor
{
    public uint cost;
    public float moveSpeed;

    protected UnitCommand currentCommand;

    public override void OnSelect()
    {

    }

    public override void OnDeselect()
    {

    }

    public void MoveTowards(Vector3 position)
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position, moveSpeed);
    }

    public UnitCommand GetCommand()
    {
        return currentCommand;
    }

    public void ReceiveCommand(UnitCommand command)
    {
        currentCommand = command;
    }

    private void Update()
    {
        if (currentCommand != null)
        {
            currentCommand.Execute();
        }
    }
}