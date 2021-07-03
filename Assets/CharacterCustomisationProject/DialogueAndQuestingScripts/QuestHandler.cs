using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public List<Quest> activeQuest;
    public List<Quest> completedQuest;

    InventoryComponent inventory;
    UnityEvent questCompletedEvent;

    #region UI stuff

    [SerializeField] GameObject questListParent;
    [SerializeField] TextMeshProUGUI questDescription;
    [SerializeField] TextMeshProUGUI questHeading;
    [SerializeField] GameObject questButtonPrefab;
    [SerializeField] Button claimRewardBtn;
    [SerializeField] TextMeshProUGUI rewardText;
    #endregion

    private void Awake()
    {
        activeQuest = new List<Quest>();
        completedQuest = new List<Quest>();

        inventory = this.GetComponent<InventoryComponent>();
        questDescription.text = "No quest description to show";
    }

    public void ReceiveQuest(Quest _q)
    {
        GatheringQuest g = (GatheringQuest)_q;
        g.completed = false;
        g.questCompleteEvent = new UnityEvent();
        g.questCompleteEvent.AddListener(delegate { CompleteQuest(_q); });
        GatheringQuestListener newListener = new GatheringQuestListener(g);
        inventory.AddListener(newListener);
        activeQuest.Add(g);
    }

    public void CompleteQuest(Quest _q)
    {
        foreach(Quest q in activeQuest)
        {
            if(q.Equals(_q))
            {
                q.SetToComplete();
            }
        }
    }

    public void ClaimReward(Quest _q)
    {
        GatheringQuest g = (GatheringQuest)_q;
        activeQuest.Remove(_q);
        completedQuest.Add(_q);
        inventory.AddItem(g.reward.itemName);
        ResetQuestDisplay();
        claimRewardBtn.gameObject.SetActive(false);
        questDescription.text = "No quest description to show";
    }
    public void DisplayActiveQuest()
    {
        ResetQuestDisplay();
        questHeading.text = "Active Quest";
        foreach(Quest q in activeQuest)
        {
            GameObject btn = Instantiate(questButtonPrefab, questListParent.transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = q.GetQuestName();
            btn.GetComponent<Button>().onClick.AddListener(delegate { ShowQuestDescription(q);});
        }
    }

    public void DisplayCompletedQuest()
    {
        ResetQuestDisplay();
        questHeading.text = "Completed Quest";
        foreach (Quest q in completedQuest)
        {
            GameObject btn = Instantiate(questButtonPrefab, questListParent.transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = q.GetQuestName();
            btn.GetComponent<Button>().onClick.AddListener(delegate { ShowCompletedQuestDescription(q); });
        }
    }

    public void ResetQuestDisplay()
    {
        for(int i = questListParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(questListParent.transform.GetChild(i).gameObject);
        }
        questDescription.text = "No Quest Description To Show";
        rewardText.gameObject.SetActive(false);
    }

    public void ShowQuestDescription(Quest _q)
    {
        questDescription.text = _q.GetQuestDescription();
        claimRewardBtn.onClick.RemoveAllListeners();
        rewardText.gameObject.SetActive(true);
        rewardText.text = "Reward: " + _q.GetQuestReward();
        if(_q.IsCompleted())
        {
            claimRewardBtn.gameObject.SetActive(true);
            claimRewardBtn.onClick.AddListener(delegate { ClaimReward(_q); });
        }
        else
        {
            claimRewardBtn.gameObject.SetActive(false);
        }
    }
    public void ShowCompletedQuestDescription(Quest _q)
    {
        questDescription.text = _q.GetQuestDescription();
    }
}
