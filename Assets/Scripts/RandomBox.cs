using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public List<int> spriteNums;
    public List<string> names;
    public List<int> amounts;

    private void Start()
    {
        spriteNums = new List<int>();
        names = new List<string>();
        amounts = new List<int>();

        LoadGifts();
    }

    void LoadGifts()
    {
        GiftList giftListData = SaveDataScript.LoadFromJson();
        if(giftListData == null)
        {
            SaveGifts();
            Debug.Log("A");
        }
        else
        {
            spriteNums = giftListData.GetSpriteNums();
            names = giftListData.GetNames();
            amounts = giftListData.GetAmounts();
        }
    }

    public void SaveGifts()
    {
        GiftList giftListData = new GiftList(spriteNums,names,amounts);
        SaveDataScript.SaveIntoJson(giftListData);
    }

    public void AddGift(int spriteNum, string name, int amount)
    {
        //이미 있는 선물인가
        int existItemPos = -1;
        for(int i = 0; i < names.Count; i++)
        {
            if(names[i] == name)
            {
                existItemPos = i;
                break;
            }
        }

        if(existItemPos != -1)
        {
            amounts[existItemPos] += amount; 
        }
        else
        {
            spriteNums.Add(spriteNum);
            names.Add(name);
            amounts.Add(amount);
        }
    }

    public void DeleteGift(int num)
    {
        names.RemoveAt(num);
        amounts.RemoveAt(num);
    }

    public void Picked(int giftNum)
    {
        amounts[giftNum] -= 1;
    }

    public bool IsItemExist()
    {
        if (names.Count == 0) return false;
        else return true;
    }
}
