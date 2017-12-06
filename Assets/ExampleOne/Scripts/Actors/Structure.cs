using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : Actor
{
    public Button economyButtonTemplate;
    public Button militaryButtonTemplate;

    public List<Unit> unitTemplates;

    private List<Button> buttonInstances = new List<Button>();

    public override void OnSelect()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<HumanPlayer>() != null)
        {
            Button economyButton = Instantiate(economyButtonTemplate);
            economyButton.transform.SetParent(GameObject.Find("Canvas").transform, false);
            economyButton.onClick.AddListener(() => CreateWorker());
            buttonInstances.Add(economyButton);

            Button militaryButton = Instantiate(militaryButtonTemplate);
            militaryButton.transform.SetParent(GameObject.Find("Canvas").transform, false);
            militaryButton.onClick.AddListener(() => CreateFighter());
            buttonInstances.Add(militaryButton);
        }
    }

    public override void OnDeselect()
    {
        foreach (Button button in buttonInstances)
        {
            Destroy(button.gameObject);
        }

        buttonInstances.Clear();
    }

    public bool CreateWorker()
    {
        uint resources = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetResourceCount();

        if (resources >= unitTemplates[0].cost)
        {
            Unit newUnit = Instantiate(unitTemplates[0]);
            int randomDegrees = Random.Range(0, 360);
            Vector3 spawnAngle = Quaternion.AngleAxis(randomDegrees, Vector3.forward) * Vector3.up;
            newUnit.transform.position = gameObject.transform.position + spawnAngle.normalized;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SubtractResources(newUnit.cost);
            return true;
        }
        else
        {
            Debug.Log("Not enough resources.");
            return false;
        }
    }

    public bool CreateFighter()
    {
        uint resources = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetResourceCount();

        if (resources >= unitTemplates[1].cost)
        {
            Unit newUnit = Instantiate(unitTemplates[1]);
            int randomDegrees = Random.Range(0, 360);
            Vector3 spawnAngle = Quaternion.AngleAxis(randomDegrees, Vector3.forward) * Vector3.up;
            newUnit.transform.position = gameObject.transform.position + spawnAngle.normalized;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SubtractResources(newUnit.cost);
            return true;
        }
        else
        {
            Debug.Log("Not enough resources.");
            return false;
        }
    }
}
