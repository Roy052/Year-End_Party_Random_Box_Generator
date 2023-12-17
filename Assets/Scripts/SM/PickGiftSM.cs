using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGiftSM : Singleton
{
    public List<Image> imgList;
    public GameObject giftPrefab;

    Dictionary<int, List<int>> giftDict = new Dictionary<int, List<int>>();
    
    private void Awake()
    {
        if (pickGiftSM)
            Destroy(this.gameObject);
        else
            pickGiftSM = this;
    }

    private void OnDestroy()
    {
        pickGiftSM = null;
    }

    private void Start()
    {
        for(int i = 0; i < Enum.GetNames(typeof(GradeType)).Length; i++)
            giftDict.Add(i, new List<int>());

        int count = sm.data.giftNameList.Count;
        for (int i = 0; i < count; i++)
            if(sm.data.giftPickedList[i] == (int)PickType.NotPicked)
                giftDict[i].Add(i);
    }

    public void Set()
    {
        int count = sm.data.giftNameList.Count;
        for(int i = 0; i < count; i++)
        {
            if (sm.data.giftPickedList[i] != (int)PickType.Current) continue;

            GameObject temp = Instantiate(giftPrefab, giftPrefab.transform.parent);
            Gift tempGift = temp.GetComponent<Gift>();
            tempGift.Set(i, sm.data.giftGradeList[i], sm.data.giftNameList[i], sm.data.giftTicketCountList[i], sm.data.giftPickedList[i]);
        }
    }

    public void PickGift(int num)
    {
        int randVal = UnityEngine.Random.Range(0, giftDict[num].Count);
        int giftNum = giftDict[num][randVal];
        sm.data.giftPickedList[giftNum] = (int)PickType.Current;
        StartCoroutine(ShowPickedGift(num, giftNum));
    }

    IEnumerator ShowPickedGift(int num, int giftNum)
    {
        for(int i = 0; i < imgList.Count; i++)
            imgList[i].gameObject.SetActive(i == num);

        RectTransform rect = imgList[num].rectTransform;

        Vector3 tempVec = new Vector3(1, 1, 0);
        while(rect.localScale.x < 1.5f)
        {
            tempVec.x += Time.deltaTime;
            tempVec.y += Time.deltaTime;

            rect.localScale = tempVec;
            yield return null;
        }
        yield return null;

        GameObject temp = Instantiate(giftPrefab, giftPrefab.transform.parent);
        Gift tempGift = temp.GetComponent<Gift>();
        tempGift.Set(giftNum, sm.data.giftGradeList[giftNum], sm.data.giftNameList[giftNum], sm.data.giftTicketCountList[giftNum], sm.data.giftPickedList[giftNum]);
    }
}
