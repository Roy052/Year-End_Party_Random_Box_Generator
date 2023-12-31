using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGiftSM : Singleton
{
    public List<Image> imgList;
    public GameObject giftPrefab;
    public ParticleSystem ps;
    public Image imgCurrent;

    public GameObject[] objTextGiftLeftBoxes;
    public Text[] textGiftLeftCounts;

    Dictionary<int, List<int>> giftDict = new Dictionary<int, List<int>>();
    List<GameObject> currentGiftList = new List<GameObject>();
    int currentNum = 0;
    
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

        for (int i = 0; i < sm.data.giftNameList.Count; i++)
        {
            if (sm.data.giftPickedList[i] == (int)PickType.NotPicked)
                giftDict[sm.data.giftGradeList[i]].Add(i);
            else if (sm.data.giftPickedList[i] == (int)PickType.Current)
                MakeElt(i);
        }

        RefreshGiftLeftText();
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
            currentGiftList.Add(temp);
        }
    }

    public void OnPickGift(int num)
    {
        currentNum = num;
        int randVal = UnityEngine.Random.Range(0, giftDict[num].Count);
        int giftNum = giftDict[num][randVal];
        sm.data.giftPickedList[giftNum] = (int)PickType.Current;
        sm.SaveData();
        imgList[num].GetComponent<Button>().enabled = false;
        StartCoroutine(ShowPickedGift(num, giftNum));
    }

    IEnumerator ShowPickedGift(int num, int giftNum)
    {
        for(int i = 0; i < imgList.Count; i++)
            imgList[i].gameObject.SetActive(i == num);

        objTextGiftLeftBoxes[num].SetActive(false);

        textGiftLeftCounts[num].transform.parent.gameObject.SetActive(false);

        RectTransform rect = imgList[num].rectTransform;

        Vector3 tempVec = new Vector3(1, 1, 0);
        while(rect.localScale.x < 1.5f)
        {
            tempVec.x += Time.deltaTime;
            tempVec.y += Time.deltaTime;

            rect.localScale = tempVec;
            yield return null;
        }

        float effectTime;
        tempVec = Vector3.zero;
        float rotVal = 0;
        for(int i = 0; i < 4; i++)
        {
            effectTime = 0;
            while(effectTime < 0.5f)
            {
                rotVal += 20 * Time.deltaTime * (i % 2 == 0  ? -1 : 1) * (effectTime > 0.25f ? -1 : 1);
                tempVec.z = rotVal;
                rect.localEulerAngles = tempVec;
                effectTime += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
        ps.Play();

        MakeElt(giftNum);

        imgCurrent.sprite = sm.sprites[giftNum];
        imgCurrent.gameObject.SetActive(true);
        giftDict[num].Remove(giftNum);
    }

    public void OnClickGift()
    {
        imgList[currentNum].rectTransform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < imgList.Count; i++)
            imgList[i].gameObject.SetActive(true);
        currentGiftList[currentGiftList.Count - 1].SetActive(true);
        imgCurrent.gameObject.SetActive(false);
        imgList[currentNum].GetComponent<Button>().enabled = true;
        RefreshGiftLeftText();
    }

    public void OnBack()
    {
        gm.ToScene("Go");
    }

    public void OnHome()
    {
        gm.ToScene("Menu");
    }

    void RefreshGiftLeftText()
    {
        for (int i = 0; i < 3; i++)
        {
            objTextGiftLeftBoxes[i].SetActive(true);
            textGiftLeftCounts[i].text = $"남은 선물 : {giftDict[i].Count}개";
        }
    }

    void MakeElt(int giftNum)
    {
        GameObject temp = Instantiate(giftPrefab, giftPrefab.transform.parent);
        Gift tempGift = temp.GetComponent<Gift>();
        tempGift.Set(giftNum, sm.data.giftGradeList[giftNum], sm.data.giftNameList[giftNum], sm.data.giftTicketCountList[giftNum], sm.data.giftPickedList[giftNum]);
        currentGiftList.Add(temp);
        temp.SetActive(true);
    }
}
