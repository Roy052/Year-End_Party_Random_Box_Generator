using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGiftSM : Singleton
{
    public GameObject playerPrefab;
    public Image imgCurrent;
    public Image imgArrow;
    public Image imgNext;
    public GameObject emptyGift;
    public GameObject emptyPlayer;
    public Text textTicketCount;

    List<int> currentGiftNumList = new List<int>();
    List<int> currentPlayerNumList = new List<int>();
    List<PlayerWithToggle> playerList = new List<PlayerWithToggle>();
    int currentPlayerOrder = 0;
    int currentGiftOrder = 0;

    private void Awake()
    {
        if (getGiftSM == null)
            getGiftSM = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        getGiftSM = null;
    }

    private void Start()
    {
        //Gift
        if(sm.data.currentGift != -1)
            currentGiftOrder = sm.data.currentGift;

        for (int i = 0; i < sm.data.giftNameList.Count; i++)
            if (sm.data.giftPickedList[i] == (int)PickType.Current)
                currentGiftNumList.Add(sm.data.giftPickedList[i]);

        //Player
        if (sm.data.currentPlayer != -1)
            currentPlayerOrder = sm.data.currentPlayer;

        Refresh();
    }

    public void OnPassGift()
    {
        currentPlayerOrder++;
        for(int i = 0; i < playerList.Count; i++) 
            playerList[i].imgNotSelected.gameObject.SetActive(i == currentPlayerOrder);
        sm.SaveData();
    }

    public void OnGetGift()
    {
        sm.data.giftPickedList[currentGiftNumList[currentGiftOrder]] = currentPlayerOrder;
        sm.data.playerTicketCountList[currentPlayerNumList[currentPlayerOrder]] -= sm.data.giftTicketCountList[currentGiftNumList[currentGiftOrder]];
        NextGift();
    }

    void NextGift()
    {
        currentGiftOrder++;
        Refresh();
        
    }

    void Refresh()
    {
        imgCurrent.gameObject.SetActive(currentGiftOrder < currentGiftNumList.Count);
        imgArrow.gameObject.SetActive(currentGiftOrder < currentGiftNumList.Count - 1);
        imgNext.gameObject.SetActive(currentGiftOrder < currentGiftNumList.Count - 1);
        emptyGift.gameObject.SetActive(currentGiftOrder >= currentGiftNumList.Count);

        //No More Current Gift
        if (currentGiftOrder >= currentGiftNumList.Count)
            return;

        imgCurrent.sprite = sm.sprites[currentGiftNumList[currentGiftOrder]];
        if(currentGiftOrder < currentGiftNumList.Count - 1)
            imgNext.sprite = sm.sprites[currentGiftNumList[currentGiftOrder + 1]];

        int currentTicketCount = sm.data.giftTicketCountList[currentGiftNumList[currentGiftOrder]];
        textTicketCount.text = $"{currentTicketCount}°³";

        if (currentGiftOrder < currentGiftNumList.Count)
            for (int i = 0; i < sm.data.playerNameList.Count; i++)
            {
                if (sm.data.playerTicketCountList[i] < currentTicketCount) 
                    continue;

                GameObject temp = Instantiate(playerPrefab, playerPrefab.transform.parent);
                temp.SetActive(true);
                PlayerWithToggle tempPlayer = temp.GetComponent<PlayerWithToggle>();
                tempPlayer.Set(sm.data.playerNameList[i], sm.data.playerTicketCountList[i]);
                playerList.Add(tempPlayer);
                currentPlayerNumList.Add(i);
            }

        emptyPlayer.SetActive(playerList.Count == 0);
    }
}
