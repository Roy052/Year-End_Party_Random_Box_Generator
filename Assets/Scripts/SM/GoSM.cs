using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoSM : Singleton
{
    public Player playerPrefab;
    public Text textInfoName;

    public GameObject empty;

    private void Awake()
    {
        if (goSM == null)
            goSM = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        goSM = null;
    }

    public void OnSetTicket()
    {
        gm.ToScene("SetTicket");
    }

    public void OnPickGift()
    {
        gm.ToScene("PickGift");
    }

    public void OnGetGift()
    {
        gm.ToScene("GetGift");
    }

    public void OnGacha()
    {
        gm.ToScene("Gacha");
    }

    public void OnSetUp()
    {
        gm.ToScene("SetUp");
    }
}
