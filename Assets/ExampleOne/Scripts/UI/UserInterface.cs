using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public AIPlayer player;

    public GameObject resourceCountText;
    public GameObject workerCountText;
    public GameObject fighterCountText;
    public GameObject currentActionText;
    public GameObject nextActionText;

    void Start()
    {

    }

    void Update ()
    {
        resourceCountText.GetComponent<Text>().text = "Resources: " + player.GetResourceCount();
        workerCountText.GetComponent<Text>().text = "Worker Count: " + GameObject.FindGameObjectsWithTag("Worker").Length + "/" + player.GetComponent<CreateFighterAction>().workersRequired;
        fighterCountText.GetComponent<Text>().text = "Fighter Count: " + GameObject.FindGameObjectsWithTag("Fighter").Length + "/" + player.totalFightersGoal;

        if (player.GetCurrentAction() != null)
        {
            currentActionText.GetComponent<Text>().text = "Current Action: " + player.GetCurrentAction().GetType().ToString();
        }
        else
        {
            currentActionText.GetComponent<Text>().text = "Current Action: -";
        }

        if (player.GetNextAction() != null)
        {
            nextActionText.GetComponent<Text>().text = "Next Action: " + player.GetNextAction().GetType().ToString();
        }
        else
        {
            nextActionText.GetComponent<Text>().text = "Next Action: -";
        }
    }
}