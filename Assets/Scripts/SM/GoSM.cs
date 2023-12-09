using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoSM : MonoBehaviour
{
    public Player playerPrefab;
    public Text textInfoName;

    public GameObject empty;

    private void Start()
    {
        
    }

    public void OnSetTicket()
    {
        GameManager.instance.ToScene("SetTicket");
    }

    public void OnPickGift()
    {
        GameManager.instance.ToScene("PickGift");
    }

    public void OnGetGift()
    {
        GameManager.instance.ToScene("GetGift");
    }

    public void OnGacha()
    {
        GameManager.instance.ToScene("Gacha");
    }

    public void OnSetUp()
    {
        GameManager.instance.ToScene("SetUp");
    }
}
