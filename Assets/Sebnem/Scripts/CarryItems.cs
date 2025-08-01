using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using JusticeScale.Scripts;

public class CarryItems : MonoBehaviour
{
    public GameObject[] items;
    public List<GameObject> allInstantiateItems;

    public bool isItemCarriable;
    public int itemNumber;

    public TextMeshProUGUI dropText;

    [TextArea]
    public string dropString;

    public TextMeshProUGUI restartText;
    public string restartString = "Press 'Q' to restart Items";

    public bool restartBool = false;

    public bool isBalanced = false;

    public Transform rightSide;
    public Transform leftSide;

    public ScaleController scaleController;
    public TriggerLift triggerLift;

    private void Update()
    {
        if (!isItemCarriable)
        {
            dropText.text = dropString;
        }
        else
        {
            dropText.text = string.Empty;
        }

        if(!isItemCarriable && Input.GetKeyDown(KeyCode.R))
        {
            Drop(rightSide);
            isItemCarriable = true;
        }
        else if(!isItemCarriable && Input.GetKeyDown(KeyCode.L))
        {
            Drop(leftSide);
            isItemCarriable = true;
        }

        if(allInstantiateItems.Count >= 4)
        {
            restartText.text = restartString;

            if (Input.GetKeyDown(KeyCode.Q)) 
            {
                foreach (GameObject item in allInstantiateItems)
                {
                    restartBool = true;
                    Destroy(item);
                }
                allInstantiateItems.Clear();
            }

            if(scaleController.WeightDifference == 1 && !isBalanced)
            {
                foreach (GameObject obj in triggerLift.objectsToLift)
                {
                    StartCoroutine(triggerLift.LiftObject(obj));
                }
                isBalanced = true;
            }
        }
    }

    void Drop(Transform transform)
    {
        GameObject allItems = Instantiate(items[itemNumber], transform.position + new Vector3 (0,0.8f,0), transform.rotation);

        allInstantiateItems.Add(allItems);
    }

}
