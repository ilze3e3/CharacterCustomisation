using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Dialogue", menuName = "Quest/New Quest Dialogue")]
public class QuestDialogue : ScriptableObject
{
    public string baseResponse;
    public string acceptedResponse;
    public string declineResponse;
    public string completedResponse;
    public Quest questToGive;
}
