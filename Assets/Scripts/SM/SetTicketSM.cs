using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTicketSM : Singleton
{
    public GameObject playerBtnPrefab;

    private void Awake()
    {
        if (setTicketSM == null)
            setTicketSM = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        setTicketSM = null;
    }

    private void Start()
    {
        int count = sm.data.playerNameList.Count;
        for(int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(playerBtnPrefab, playerBtnPrefab.transform.parent);
            temp.SetActive(true);
            PlayerWithButton pbp = temp.GetComponent<PlayerWithButton>();
            pbp.Set(i, sm.data.playerNameList[i], sm.data.playerTicketCountList[i]);
        }
    }
}
