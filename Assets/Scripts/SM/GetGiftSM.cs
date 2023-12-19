using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGiftSM : Singleton
{
    public GameObject playerPrefab;
    public Image imgCurrent;
    public Image imgNext;

    List<int> currentGiftNumList = new List<int>();
    int currentPlayer = 0;
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
        int count = sm.data.playerNameList.Count;

        for(int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(playerPrefab, playerPrefab.transform.parent);
            temp.SetActive(true);
            Player tempPlayer = temp.GetComponent<Player>();
            tempPlayer.Set(sm.data.playerNameList[i], sm.data.playerTicketCountList[i]);
        }

        count = sm.data.giftNameList.Count;
        for(int i = 0; i < count; i++)
            if (sm.data.giftPickedList[i] == (int)PickType.Current)
                currentGiftNumList.Add(sm.data.giftPickedList[i]);
    }
}
