using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObj = GetClickedObject();

            if (clickedObj != null)
            {
                SelectActor(clickedObj);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject clickedObj = GetClickedObject();

            if (clickedObj != null)
            {
                switch (clickedObj.tag)
                {
                    case "ResourceNode":
                        IssueGatherCommand(selectedActor, clickedObj);
                        break;
                }
            }
            else
            {
                if (selectedActor != null)
                {
                    IssueMoveCommand(selectedActor, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
    }

    protected GameObject GetClickedObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
