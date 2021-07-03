using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPCDialogue", menuName = "Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public List<QuestDialogue> availableQuest;
    public int currentApprovalRating;
    public List<Response> neutralDialogueList;
    public List<Response> likeDialogueList;
    public List<Response> dislikeDialogueList;

    public int minLikeRating;
    public int minDislikeRating;

    public int insultRatingCost;
    public int complimentRatingCost;

    public SingleResponse greetingResponse;
    public SingleResponse complimentResponse;
    public SingleResponse insultResponse;
}
