using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCDialogue dialogue;

    public List<QuestDialogue> availableQuest;

    public int currentApprovalRating;
    // Start is called before the first frame update
    private void Start()
    {
        foreach(QuestDialogue q in dialogue.availableQuest)
        {
            availableQuest.Add(q);
        }

        currentApprovalRating = dialogue.currentApprovalRating;
    }
}
