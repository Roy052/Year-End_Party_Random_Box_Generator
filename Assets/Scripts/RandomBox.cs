using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public List<string> names;
    public List<int> amounts;

    private void Start()
    {
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
            List<Gift> giftList = giftListData.GetGiftList();
            for (int i = 0; i < giftList.Count; i++)
            {
                AddGift(giftList[i].GetName(), giftList[i].GetAmount());
            }
        }
    }

    public void SaveGifts()
    {
        GiftList giftListData;
        List<Gift> giftList = new List<Gift>();
        for(int i = 0; i < names.Count; i++)
        {
            giftList.Add(new Gift(names[i], amounts[i]));
        }
        giftListData = new GiftList(giftList);
        SaveDataScript.SaveIntoJson(giftListData);
    }

    public void AddGift(string name, int amount)
    {
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
