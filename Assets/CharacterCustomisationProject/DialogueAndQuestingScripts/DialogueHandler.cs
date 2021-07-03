using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public enum Rating { Dislike, Neutral, Like };

    [SerializeField] NPCDialogue currentNPC;
    [SerializeField] QuestHandler questHandler;

    [SerializeField] List<QuestDialogue> availableQuest;

    #region UI Gameobjects
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject choiceButtonPrefab;
    #endregion

    private void Awake()
    {
        questHandler = this.GetComponent<QuestHandler>();
    }
    /// <summary>
    /// When starting a conversation with an NPC or after finishing a previous conversation show these options.
    /// </summary>
    public void InitiateConversation()
    {
        ResetChoices();
        
        foreach(QuestDialogue q in availableQuest)
        {
            GameObject questBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
            questBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Quest available: " + q.questToGive.GetQuestName();
            questBtn.GetComponent<Button>().onClick.AddListener(delegate { DisplayQuest(q); });
        }

        dialogueText.text = currentNPC.greetingResponse.GetSentence();
        GameObject talkBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        talkBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Talk";
        talkBtn.GetComponent<Button>().onClick.AddListener(delegate { Talk(); });

        GameObject complimentBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        complimentBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Compliment";
        complimentBtn.GetComponent<Button>().onClick.AddListener(delegate { Compliment(); });

        GameObject insultBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        insultBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Insult";
        insultBtn.GetComponent<Button>().onClick.AddListener(delegate { Insult(); });
        
        GameObject goAwayBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        goAwayBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Stop Talking";
        goAwayBtn.GetComponent<Button>().onClick.AddListener(delegate { GoAway(); });
    }

    /// <summary>
    /// Talks about a random subject with an NPC
    /// </summary>
    public void Talk()
    {
        /// Check approval rating
        /// if over minLikeRating then its like
        /// if below minDislikeRating then its dislike
        /// if between then neutral
        /// then choose from respective dialogue list
        /// 
        ResetChoices();

        switch (GetRating())
        {
            case Rating.Like:
                // Display one of the dialogue from the Like Dialogue List
                Response tmpLike = currentNPC.likeDialogueList[Random.Range(0, currentNPC.likeDialogueList.Count)];
                switch(tmpLike.IsMultiple())
                {
                    case true:
                        MultipleResponse ml = (MultipleResponse)tmpLike;
                        dialogueText.text = ml.baseResponse;
                        // Spawn Button Prefabs and attach function to show the response based on the reply;
                        foreach (MultipleChoice m in ml.multipleChoiceResponse)
                        {
                            // Instantiate Button and parent it to the panel
                            GameObject button = Instantiate(choiceButtonPrefab, choicePanel.transform);
                            button.GetComponentInChildren<TextMeshProUGUI>().text = m.choice;
                            // Add function to it that displays text using the response field
                            button.GetComponent<Button>().onClick.AddListener(delegate { DisplayText(m.response); });
                        }
                            break;
                    case false:
                        SingleResponse sl = (SingleResponse)tmpLike;
                        dialogueText.text = sl.GetSentence();
                        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
                        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
                        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });
                        break;
                }
                break;
            case Rating.Neutral:
                // Display one of the dialogue from the Neutral Dialogue List
                Response tmpNeutral = currentNPC.neutralDialogueList[Random.Range(0, currentNPC.neutralDialogueList.Count)];
                switch (tmpNeutral.IsMultiple())
                {
                    case true:
                        MultipleResponse mn = (MultipleResponse)tmpNeutral;
                        dialogueText.text = mn.baseResponse;
                        foreach (MultipleChoice m in mn.multipleChoiceResponse)
                        {
                            // Instantiate Button and parent it to the panel
                            GameObject button = Instantiate(choiceButtonPrefab, choicePanel.transform);
                            button.GetComponentInChildren<TextMeshProUGUI>().text = m.choice;
                            // Add function to it that displays text using the response field
                            button.GetComponent<Button>().onClick.AddListener(delegate { DisplayText(m.response); });
                        }
                        break;
                    case false:
                        SingleResponse sn = (SingleResponse)tmpNeutral;
                        dialogueText.text = sn.GetSentence();
                        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
                        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
                        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });
                        break;
                }
                break;
            case Rating.Dislike:
                // Display one of the dialogue from the Dislike Dialogue list
                Response tmpDislike = currentNPC.dislikeDialogueList[Random.Range(0, currentNPC.dislikeDialogueList.Count)];
                switch (tmpDislike.IsMultiple())
                {
                    case true:
                        MultipleResponse md = (MultipleResponse)tmpDislike;
                        dialogueText.text = md.baseResponse;
                        foreach (MultipleChoice m in md.multipleChoiceResponse)
                        {
                            // Instantiate Button and parent it to the panel
                            GameObject button = Instantiate(choiceButtonPrefab, choicePanel.transform);
                            button.GetComponentInChildren<TextMeshProUGUI>().text = m.choice;
                            // Add function to it that displays text using the response field
                            button.GetComponent<Button>().onClick.AddListener(delegate { DisplayText(m.response); });
                        }
                        break;
                    case false:
                        SingleResponse sd = (SingleResponse)tmpDislike;
                        dialogueText.text = sd.GetSentence();
                        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
                        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
                        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });
                        break;
                }
                break;
        }

    }
    /// <summary>
    /// Compliment NPC increasing approval rating
    /// </summary>
    public void Compliment()
    {
        // Add rating and display response
        currentNPC.currentApprovalRating += currentNPC.complimentRatingCost;
        if(currentNPC.currentApprovalRating > 100)
        {
            currentNPC.currentApprovalRating = 100;
        }
        //Display Response
        dialogueText.text = currentNPC.complimentResponse.GetSentence();
        
        ResetChoices();

        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });

    }
    /// <summary>
    /// Insulting the NPC lowers the approval rating
    /// </summary>
    public void Insult()
    {
        //Subtract rating and display response
        currentNPC.currentApprovalRating += currentNPC.insultRatingCost;
        if (currentNPC.currentApprovalRating < 0)
        {
            currentNPC.currentApprovalRating = 0;
        }
        //Display Response
        dialogueText.text = currentNPC.insultResponse.GetSentence();

        ResetChoices();

        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });
    }

    public Rating GetRating()
    {
        if (currentNPC.currentApprovalRating > currentNPC.minLikeRating)
        {
            return Rating.Like;
        }
        else if (currentNPC.currentApprovalRating < currentNPC.minDislikeRating)
        {
            return Rating.Dislike;
        }

        return Rating.Neutral;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            currentNPC = collision.gameObject.GetComponent<NPC>().dialogue;
            availableQuest = collision.gameObject.GetComponent<NPC>().availableQuest;
            dialoguePanel.SetActive(true);
            InitiateConversation();
        }
    }
    /// <summary>
    /// Display text for single responses
    /// </summary>
    /// <param name="_text"></param>
    private void DisplayText(string _text)
    {
        dialogueText.text = _text;

        ResetChoices();

        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });
    }
    /// <summary>
    /// Resets all the choices in the panel by destroying them
    /// </summary>
    public void ResetChoices()
    {
        int childCount = choicePanel.transform.childCount;
        Debug.Log("ChildCount: " + childCount);
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(choicePanel.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// Stop the Dialogue and turn the panel off.
    /// </summary>
    public void GoAway()
    {
        ResetChoices();
        dialoguePanel.SetActive(false);
    }
    public void DisplayQuest(QuestDialogue _q)
    {
        ResetChoices();
        dialogueText.text = _q.baseResponse;
        GameObject acceptBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        acceptBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Accept Quest";
        // Add listener to display response and add quest to quest list if accepted;
        acceptBtn.GetComponent<Button>().onClick.AddListener(delegate { AcceptQuest(_q); });

        GameObject declineBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        declineBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Decline Quest";
        // Add listener to display response if declined;
        declineBtn.GetComponent<Button>().onClick.AddListener(delegate { DeclineQuest(_q); });
    }

    public void AcceptQuest(QuestDialogue _q)
    {
        ResetChoices();
        dialogueText.text = _q.acceptedResponse;

        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });

        availableQuest.Remove(_q);
        questHandler.ReceiveQuest(_q.questToGive);
    }
    public void DeclineQuest(QuestDialogue _q)
    {
        ResetChoices();
        dialogueText.text = _q.declineResponse;

        GameObject nextBtn = Instantiate(choiceButtonPrefab, choicePanel.transform);
        nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        nextBtn.GetComponent<Button>().onClick.AddListener(delegate { InitiateConversation(); });

    }
}
