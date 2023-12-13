using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpSM : Singleton
{
    //Player ScrollView
    [SerializeField] GameObject playerPrefab;
    List<Player> playerList;

    //Gift ScrollView
    [SerializeField] GameObject giftPrefab;
    [SerializeField] Text tableHead;
    [SerializeField] Sprite[] giftSprites;
    List<GiftButton> giftList;

    //Player InputDatas
    [SerializeField] InputField[] inputFieldPlayer;

    string tempPlayerName;
    string tempPlayerValue;

    //Gift InputDatas
    [SerializeField] Dropdown dropDownGradeNum;
    [SerializeField] InputField[] inputFieldGift;
    [SerializeField] Text textWarningPlayer;
    [SerializeField] Text textWarningGift;
    [SerializeField] Image giftImage;

    int giftCount = 0;
    int tempGradeNum;
    string tempGiftName;
    string tempGiftValue;

    //Audio
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    private void Awake()
    {
        if (setUpSM == null)
            setUpSM = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        setUpSM = null;
    }

    private void Start()
    {
        playerList = new List<Player>();
        giftList = new List<GiftButton>();
        audioSource = GetComponent<AudioSource>();

        giftCount = sm.data.giftNameList.Count;
        for (int i = 0; i < giftCount; i++)
            CreateGiftPrefab(i, sm.data.giftGradeList[i], sm.data.giftNameList[i], sm.data.giftValueList[i]);

        dropDownGradeNum.options.Clear();
        for (int i = 1; i <= 3; i++)
            dropDownGradeNum.options.Add(new Dropdown.OptionData() { text = Extended.ConvertToRoman(i) });
            
        if(sm.sprites.Count > giftCount + 1)
            giftImage.sprite = sm.sprites[giftCount + 1];
    }

    public void OnAddPlayer()
    {
        tempPlayerName = inputFieldPlayer[0].text;
        tempPlayerValue = inputFieldPlayer[1].text;

        if (tempPlayerName == "")
        {
            textWarningPlayer.text = "이름을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
        }
        else if (tempPlayerValue == "")
        {
            textWarningPlayer.text = "수량을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
        }
        else
        {
            try
            {
                CreatePlayer(tempPlayerName, int.Parse(tempPlayerValue));
                AudioON("add");
            }
            catch
            {
                textWarningPlayer.text = "수량은 숫자로 입력하세요";
                StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
            }
        }
    }

    public void OnAddGift()
    {
        giftCount = giftList.Count;
        tempGradeNum = dropDownGradeNum.value;
        tempGiftName = inputFieldGift[0].text;
        tempGiftValue = inputFieldGift[1].text;
        
        if(tempGiftName == "")
        {
            textWarningGift.text = "이름을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
        }
        else if(tempGiftValue == "")
        {
            textWarningGift.text = "수량을 입력하세요";
            StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
        }
        else
        {
            try
            {
                CreateGift(giftCount, tempGradeNum, tempGiftName, int.Parse(tempGiftValue));
                AudioON("add");
            }
            catch
            {
                textWarningGift.text = "수량은 숫자로 입력하세요";
                StartCoroutine(FadeManager.FadeOut(textWarningGift, 3));
            }
        }
    }

    public void CreatePlayer(string playerName, int ticketCount)
    {
        int idx = sm.data.playerNameList.IndexOf(playerName);

        sm.AddPlayer(playerName, ticketCount);
        if (idx == -1)
            CreatePlayerPrefab(playerName, ticketCount);
        else
            playerList[idx].transform.GetChild(2).GetComponent<Text>().text = ticketCount + "티켓";
    }

    public void CreateGift(int num, int giftGrade, string giftName,int giftValue)
    {
        int idx = sm.data.giftNameList.IndexOf(giftName);

        sm.AddGift(giftName, giftGrade, giftValue);
        if (idx == -1)
            CreateGiftPrefab(num, giftGrade, giftName, giftValue);
        else
            giftList[idx].transform.GetChild(2).GetComponent<Text>().text = sm.data.giftValueList[idx] + "티켓";
    }

    public void CreatePlayerPrefab(string playerName, int ticketCount)
    {
        GameObject temp = Instantiate(playerPrefab, playerPrefab.transform.parent);
        temp.transform.GetChild(1).GetComponent<Text>().text = playerName;
        temp.transform.GetChild(2).GetComponent<Text>().text = ticketCount.ToString();
        temp.SetActive(true);

        Player player = temp.GetComponent<Player>();
        playerList.Add(player);
        AudioON("add");
    }

    public void CreateGiftPrefab(int num, int giftGrade, string giftName, int giftValue)
    {
        GameObject temp = Instantiate(giftPrefab, giftPrefab.transform.parent);
        temp.transform.GetChild(0).GetComponent<Text>().text = $"- {Extended.ConvertToRoman(giftGrade + 1)} -";
        temp.transform.GetChild(1).GetComponent<Text>().text = giftName;
        temp.transform.GetChild(2).GetComponent<Text>().text = giftValue + "개";
        temp.SetActive(true);

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

        var msg = gm.OpenConfirmMsg();
        msg.Set("데이터 리셋", () =>
        {
            sm.DataReset();
            SaveDataScript.DeleteSave();
        });
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
