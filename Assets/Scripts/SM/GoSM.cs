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

    public void ToSetTicket()
    {
        GameManager.instance.ToScene("SetTicket");
    }

    public void ToSetGift()
    {
        GameManager.instance.ToScene("SetGift");
    }

    public void ToPickGift()
    {
        GameManager.instance.ToScene("PickGift");
    }

    public void ToGacha()
    {
        GameManager.instance.ToScene("Gacha");
    }

    public void ToSetUp()
    {
        GameManager.instance.ToScene("SetUp");
    }
}
