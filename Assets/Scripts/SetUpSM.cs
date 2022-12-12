using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpSM : MonoBehaviour
{
    GameObject itemObject;
    [SerializeField] GameObject giftTemplate;
    [SerializeField] Transform giftTransform;
    [SerializeField] Text tableHead;
    [SerializeField] Sprite[] giftSprites;
    RandomBox randomBox;
    GameManager gm;
    public List<GameObject> giftButtons;

    //InputDatas
    [SerializeField] InputField inputFields;
    int tempNum;
    string tempName, tempAmount;

    private void Start()
    {
        giftButtons = new List<GameObject>();
        randomBox = GameObject.Find("GameManager").GetComponent<RandomBox>();

        for(int i = 0; i < 5; i++)
        {
            CreateGift(i, "A " + i, 1);
        }
    }

    public void CreateGift()
    {
        if(tempName != "" && tempAmount != "")
        {
            CreateGift(tempNum, tempName, int.Parse(tempAmount));
        }
    }

    public void CreateGift(int num, string giftName,int giftAmount)
    {
        GameObject temp = Instantiate(giftTemplate, giftTransform);
        temp.transform.GetChild(0).GetComponent<Text>().text = giftName;
        temp.transform.GetChild(1).GetComponent<Text>().text = giftAmount + "°³";

        randomBox.AddGift(giftName, giftAmount);

        temp.GetComponent<GiftButton>().num = num;
        temp.GetComponent<GiftButton>().setUpSM = this;
        giftButtons.Add(temp);
    }

    public void DeleteGift(int num)
    {
        randomBox.DeleteGift(num);
        Destroy(giftButtons[num]);
        giftButtons.RemoveAt(num);

        for (int i = num; i < giftButtons.Count; i++)
        {
            giftButtons[i].GetComponent<GiftButton>().num -= 1;
        }
    }

    public void SaveGifts()
    {
        randomBox.SaveGifts();
    }
}
