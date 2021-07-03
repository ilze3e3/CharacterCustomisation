using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatheringQuestListener : Listener
{
    public GatheringQuestListener(GatheringQuest _referenceToQuest)
    {
        referenceToQuest = _referenceToQuest;
        itemToLookFor = referenceToQuest.itemToGather;
        quantityToLookFor = referenceToQuest.quantity;
        currentQuantity = 0;
        isCompleted = false;
    }

    public Item itemToLookFor;
    public int currentQuantity;
    public int quantityToLookFor;
    public bool isCompleted;
    public GatheringQuest referenceToQuest;

    public override void CheckListener(Item itemToCheck)
    {
        Debug.Log("Checking listener. Item is: " + itemToCheck.itemName);
        if(itemToCheck.itemName == itemToLookFor.itemName)
        {
            Debug.Log("Item is the same");
            currentQuantity++;
            if (currentQuantity == quantityToLookFor)
            {
                Debug.Log("Quest Complete");
                isCompleted = true;
                referenceToQuest.CompleteQuest();
            }
        }
    }
}
