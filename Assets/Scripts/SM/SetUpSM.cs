using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpSM : MonoBehaviour
{
    GameObject itemObject;
    [SerializeField] GameObject giftPrefab;
    [SerializeField] Transform giftTransform;
    [SerializeField] Text tableHead;
    [SerializeField] Sprite[] giftSprites;
    GameManager gm;
    SaveManager sm;
    List<GiftButton> giftList;

    //InputDatas
    [SerializeField] Dropdown dropDownGradeNum;
    [SerializeField] InputField[] inputFields;
    [SerializeField] Text warningText;
    [SerializeField] Image giftImage;
    int giftCount = 0;
    int tempGradeNum;
    string tempName, tempValue;

    //Audio
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    private void Start()
    {
        giftList = new List<GiftButton>();
        gm = GameManager.instance;
        sm = SaveManager.instance;
        audioSource = this.GetComponent<AudioSource>();

        giftCount = sm.data.giftNameList.Count;
        for (int i = 0; i < giftCount; i++)
            CreateGiftBoard(i, sm.data.giftGradeList[i], sm.data.giftNameList[i], sm.data.giftValueList[i]);

        dropDownGradeNum.options.Clear();
        for (int i = 1; i <= 3; i++)
            dropDownGradeNum.options.Add(new Dropdown.OptionData() { text = Extended.ConvertToRoman(i) });
        dropDownGradeNum.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDownGradeNum); });
            
        if(sm.sprites.Count > giftCount + 1)
            giftImage.sprite = sm.sprites[giftCount + 1];
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        giftImage.sprite = sm.sprites[index];
    }

    public void CreateGift()
    {
        giftCount = giftList.Count;
        tempGradeNum = dropDownGradeNum.value;
        tempName = inputFields[0].text;
        tempValue = inputFields[1].text;
        
        if(tempName == "")
        {
            warningText.text = "이름을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(warningText, 3));
        }
        else if(tempValue == "")
        {
            warningText.text = "수량을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(warningText, 3));
        }
        else
        {
            try
            {
                CreateGift(giftCount, tempGradeNum, tempName, int.Parse(tempValue));
                AudioON("add");
            }
            catch
            {
                warningText.text = "수량은 숫자로 입력하세요";
                StartCoroutine(FadeManager.FadeOut(warningText, 3));
            }
        }
    }

    public void CreateGift(int num, int giftGrade, string giftName,int giftValue)
    {
        bool itemExist = false;
        int itemPos = -1;
        for(int i = 0; i < giftList.Count; i++)
        {
            if (sm.data.giftNameList[i] == giftName)
            {
                itemExist = true;
                itemPos = i;
                break;
            }
        }

        sm.AddGift(giftName, giftGrade, giftValue);

        if (itemExist)
        {
            giftList[itemPos].transform.GetChild(1).GetComponent<Text>().text = sm.data.giftValueList[itemPos] + "티켓";
        }
        else
        {
            CreateGiftBoard(num, giftGrade, giftName, giftValue);
        }
    }

    public void CreateGiftBoard(int num, int giftGrade, string giftName, int giftValue)
    {
        GameObject temp = Instantiate(giftPrefab, giftTransform);
        temp.transform.GetChild(0).GetComponent<Text>().text = $"- {Extended.ConvertToRoman(giftGrade)} -";
        temp.transform.GetChild(1).GetComponent<Text>().text = giftName;
        temp.transform.GetChild(2).GetComponent<Text>().text = giftValue + "개";

        GiftButton giftButton = temp.GetComponent<GiftButton>();
        giftButton.num = num;
        giftButton.setUpSM = this;
        giftList.Add(giftButton);
        AudioON("add");
    }

    public void DeleteGift(int num)
    {
        sm.DeleteGift(num);
        Destroy(giftList[num]);
        giftList.RemoveAt(num);

        for (int i = num; i < giftList.Count; i++)
        {
            giftList[i].GetComponent<GiftButton>().num -= 1;
        }
    }

    public void SaveGifts()
    {
        AudioON("save");
        sm.SaveData();
    }

    public void ResetGifts()
    {
        AudioON("reset");
        while (giftList.Count != 0)
            DeleteGift(0);
        

        //SaveDataScript.DeleteSave();
    }

    public void ReloadImage()
    {
        sm.LoadImage();
        if(sm.sprites.Count > giftCount)
            giftImage.sprite = sm.sprites[giftCount];
    }

    public void ToMenu()
    {
        gm.ToMenu();
    }

    void AudioON(string clipName)
    {
        if(clipName == "add")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if (clipName == "reset")
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if (clipName == "save")
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }
}
