using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Gathering Quest", menuName = "Quest/New GatheringQuest")]
public class GatheringQuest : Quest
{
    public string questName;
    public bool completed = false;
    public string questDescription;
    public Item itemToGather;
    public int quantity;
    public Item reward;
    public UnityEvent questCompleteEvent;
    public void CompleteQuest()
    {
        completed = true;
        questCompleteEvent.Invoke();
    }
    public override bool IsCompleted()
    {
        return completed;
    }

    public override string GetQuestName()
    {
        return questName;
    }
    public override string GetQuestDescription()
    {
        return questDescription;
    }
    public override void SetToComplete()
    {
        completed = true;
    }
    public override string GetQuestReward()
    {
        return reward.itemName + " (1)";
    }
}
