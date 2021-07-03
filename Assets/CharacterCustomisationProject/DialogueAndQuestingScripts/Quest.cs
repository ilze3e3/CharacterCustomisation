using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public abstract string GetQuestName();
    public abstract string GetQuestDescription();
    public abstract bool IsCompleted();
    public abstract void SetToComplete();

    public abstract string GetQuestReward();
}
